using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InterSim.Application.Interviews.Evaluation;

public static class BehavioralEvaluatorV1
{
    public const string EvaluatorVersion = "heuristic-eval-v1";

    private const int TooShortWords = 60;
    private const int TooLongWords = 350;

    private static readonly Regex NumberRegex = new(@"\d+", RegexOptions.Compiled);

    
    private static readonly string[] SituationMarkers =
    {
        "in a project",
        "at my job",
        "at work",
        "at university",
        "in a team",
        "there was a problem",
        "there was a challenge",
        "the context was"
    };

    private static readonly string[] TaskMarkers =
    {
        "my responsibility was",
        "my goal was",
        "the objective was",
        "i was responsible for",
        "i had to",
        "i needed to"
    };

    private static readonly string[] ActionMarkers =
    {
        "i did",
        "i implemented",
        "i decided",
        "i chose",
        "i discussed",
        "i negotiated",
        "i analyzed",
        "i optimised",
        "i optimized",
        "i resolved",
        "i built",
        "i designed"
    };

    private static readonly string[] ResultMarkers =
    {
        "as a result",
        "in the end",
        "this led to",
        "the impact was",
        "i achieved",
        "we achieved",
        "we reduced",
        "we increased",
        "it improved",
        "it decreased"
    };

    private static readonly string[] ReflectionMarkers =
    {
        "i learned",
        "next time",
        "i would",
        "i would do differently",
        "this taught me",
        "the lesson was"
    };

    public static AnswerEvaluationResult Evaluate(string questionText, string answerText)
    {
        questionText ??= string.Empty;
        answerText ??= string.Empty;

        var qNorm = Normalize(questionText);
        var aNorm = Normalize(answerText);

        var aWordCount = CountWords(aNorm);
        var deficits = new List<DeficitTag>();

        
        if (aWordCount < TooShortWords) deficits.Add(DeficitTag.TooShort);
        if (aWordCount > TooLongWords) deficits.Add(DeficitTag.TooLong);

       
        var overlapRatio = ComputeKeywordOverlapRatio(qNorm, aNorm);

       
        var hasTopicHit = overlapRatio >= 0.2;

        var offTopic = overlapRatio < 0.2 && !hasTopicHit;
        if (offTopic) deficits.Add(DeficitTag.OffTopic);

        
        var hasSituation = ContainsAny(aNorm, SituationMarkers);
        var hasTask = ContainsAny(aNorm, TaskMarkers);
        var hasAction = ContainsAny(aNorm, ActionMarkers);
        var hasResult = ContainsAny(aNorm, ResultMarkers);

        if (!hasTask) deficits.Add(DeficitTag.MissingTask);
        if (!hasAction) deficits.Add(DeficitTag.MissingAction);
        if (!hasResult) deficits.Add(DeficitTag.MissingResult);

        var missingStarCount = new[] { hasSituation, hasTask, hasAction, hasResult }.Count(x => x == false);
        if (missingStarCount >= 2) deficits.Add(DeficitTag.LowStructure);

        
        var numberCount = NumberRegex.Matches(aNorm).Count;
        var hasMetrics = numberCount > 0;
        if (hasResult && !hasMetrics) deficits.Add(DeficitTag.MissingMetrics);

        var actionVerbCount = CountActionVerbHits(aNorm);
        var actionPer50 = aWordCount > 0 ? (actionVerbCount * 50.0) / aWordCount : 0.0;

        
        var hasI = aNorm.Contains(" i ");
        var hasWe = aNorm.Contains(" we ");
        if (!hasI && hasWe) deficits.Add(DeficitTag.NoOwnership);

       
        var hasConcreteImpact = hasMetrics || ContainsAny(
            aNorm,
            "reduced",
            "increased",
            "improved",
            "decreased",
            "%",
            "ms",
            "sec",
            "seconds",
            "minutes",
            "hours",
            "days",
            "weeks"
        );

        if (hasResult && !hasConcreteImpact) deficits.Add(DeficitTag.VagueOutcome);

        
        var hasReflection = ContainsAny(aNorm, ReflectionMarkers);
        if (!hasReflection) deficits.Add(DeficitTag.NoReflection);

       
        var hasArtifactsOrTools = ContainsAny(
            aNorm,
            "endpoint",
            "api",
            "db",
            "database",
            "docker",
            "test",
            "tests",
            "pull request",
            "pr",
            "ticket",
            "diagram",
            "deploy",
            "deployment",
            "ci/cd",
            "postgres",
            "ef core",
            "logging",
            "monitoring"
        );

        var specificitySignals = (hasMetrics ? 1 : 0) + (hasArtifactsOrTools ? 1 : 0) + (actionVerbCount > 0 ? 1 : 0);
        if (specificitySignals <= 1) deficits.Add(DeficitTag.LowSpecificity);

       
        var breakdown = new List<AnswerEvaluationBreakdownItem>(4);

        
        var relevance = ScoreRelevance(overlapRatio, hasTopicHit, offTopic);
        breakdown.Add(new("Relevance", relevance, 25, $"overlap={overlapRatio:0.00}"));

        
        var structure = ScoreStructure(hasSituation, hasTask, hasAction, hasResult, aNorm);
        breakdown.Add(new("Structure", structure, 25, $"S={hasSituation},T={hasTask},A={hasAction},R={hasResult}"));

        
        var specificity = ScoreSpecificity(numberCount, actionPer50, hasArtifactsOrTools);
        breakdown.Add(new("Specificity", specificity, 25, $"numbers={numberCount}, actionHits={actionVerbCount}, artifacts={hasArtifactsOrTools}"));

        
        var impact = ScoreImpact(hasResult, hasConcreteImpact, hasReflection, hasMetrics);
        breakdown.Add(new("Impact & Learning", impact, 25, $"result={hasResult}, metrics={hasMetrics}, reflection={hasReflection}"));

        var total = relevance + structure + specificity + impact;

        
        if (aWordCount < TooShortWords)
        {
            total = (int)Math.Round(total * 0.6, MidpointRounding.AwayFromZero);
        }

        if (aWordCount > TooLongWords)
        {
            total = Math.Max(0, total - 5);
        }

        total = Math.Clamp(total, 0, 100);

        var distinctDeficits = deficits
            .Distinct()
            .OrderBy(d => (int)d)
            .ToList();

        return new AnswerEvaluationResult(
            Score: total,
            MaxScore: 100,
            Breakdown: breakdown,
            Deficits: distinctDeficits
        );
    }

    private static int ScoreRelevance(double overlapRatio, bool hasTopicHit, bool offTopic)
    {
        if (offTopic) return 0;

        var baseScore =
            overlapRatio >= 0.6 ? 20 :
            overlapRatio >= 0.4 ? 15 :
            overlapRatio >= 0.2 ? 10 :
            5;

        var bonus = hasTopicHit ? 5 : 0;
        return Math.Clamp(baseScore + bonus, 0, 25);
    }

    private static int ScoreStructure(bool s, bool t, bool a, bool r, string answerNorm)
    {
        var score = 0;
        if (s) score += 6;
        if (t) score += 6;
        if (a) score += 6;
        if (r) score += 6;

       
        var idxS = FirstIndexOfAny(answerNorm, SituationMarkers);
        var idxT = FirstIndexOfAny(answerNorm, TaskMarkers);
        var idxA = FirstIndexOfAny(answerNorm, ActionMarkers);
        var idxR = FirstIndexOfAny(answerNorm, ResultMarkers);

        var ordered = IsNonDecreasing(new[] { idxS, idxT, idxA, idxR }.Where(i => i >= 0).ToArray());
        if (ordered && (s || t || a || r)) score += 1;

        return Math.Clamp(score, 0, 25);
    }

    private static int ScoreSpecificity(int numberCount, double actionPer50, bool hasArtifactsOrTools)
    {
        var metrics =
            numberCount >= 3 ? 10 :
            numberCount >= 1 ? 5 :
            0;

        var action =
            actionPer50 >= 1.0 ? 8 :
            actionPer50 >= (50.0 / 80.0) ? 5 : 
            0;

        var artifacts = hasArtifactsOrTools ? 4 : 0;

        var total = metrics + action + artifacts;
        return Math.Clamp(total, 0, 25);
    }

    private static int ScoreImpact(bool hasResult, bool hasConcreteImpact, bool hasReflection, bool hasMetrics)
    {
        var resultClarity = hasResult ? (hasConcreteImpact ? 10 : 5) : 0;
        var metricImpact = (hasResult && hasMetrics) ? 7 : 0;
        var reflection = hasReflection ? 8 : 0;

        var total = resultClarity + metricImpact + reflection;
        return Math.Clamp(total, 0, 25);
    }

    private static string Normalize(string s)
    {
        s = (s ?? string.Empty).Trim().ToLowerInvariant();

        
        s = Regex.Replace(s, @"[,\.\!\?\;\:\(\)""']", " ");

       
        s = Regex.Replace(s, @"\s+", " ");

        return " " + s + " ";
    }

    private static int CountWords(string norm)
        => norm.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

    private static bool ContainsAny(string norm, params string[] needles)
        => needles.Any(n => norm.Contains(" " + n.Trim().ToLowerInvariant() + " "));

    private static int CountActionVerbHits(string norm)
        => ActionMarkers.Count(m => norm.Contains(" " + m.Trim().ToLowerInvariant() + " "));

    private static double ComputeKeywordOverlapRatio(string questionNorm, string answerNorm)
    {
        var qTokens = ExtractImportantKeywords(questionNorm);
        if (qTokens.Count == 0) return 0.0;

        var aTokens = ExtractImportantKeywords(answerNorm);

        var matched = qTokens.Count(t => aTokens.Contains(t));
        return matched / (double)qTokens.Count;
    }

    private static HashSet<string> ExtractImportantKeywords(string norm)
    {
       
        var stop = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "this","that","what","when","where","which","with","from","your","about",
            "time","tell","me","have","been","were","they","them","into","onto","over",
            "and","the","a","an","to","of","in","on","for","as","at","it","is","are",
            "was","i","we","you","he","she","my","our","their"
        };

        var tokens = norm
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim().Trim(',', '.', '?', '!', ';', ':', '"', '\'', '(', ')'))
            .Where(t => t.Length >= 4 && !stop.Contains(t))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return tokens;
    }

    private static int FirstIndexOfAny(string norm, string[] needles)
    {
        var best = -1;
        foreach (var n in needles)
        {
            var idx = norm.IndexOf(" " + n.Trim().ToLowerInvariant() + " ", StringComparison.Ordinal);
            if (idx >= 0 && (best < 0 || idx < best)) best = idx;
        }
        return best;
    }

    private static bool IsNonDecreasing(int[] values)
    {
        for (var i = 1; i < values.Length; i++)
            if (values[i] < values[i - 1]) return false;
        return true;
    }
}