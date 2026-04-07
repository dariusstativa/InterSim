using InterSim.Application.Abstractions;

namespace InterSim.Application.Interviews.Evaluation;

public sealed class BehavioralEvaluatorV2
{
    private readonly IEmbeddingService _embeddingService;

    public BehavioralEvaluatorV2(IEmbeddingService embeddingService)
    {
        _embeddingService = embeddingService;
    }

    public async Task<double> ComputeSemanticRelevanceAsync(
        string questionText,
        string answerText,
        CancellationToken ct = default)
    {
        var result = await ComputeSemanticSignalsAsync(questionText, answerText, ct);

        return (0.4 * result.SimQA) +
               (0.4 * result.MaxChunkSim) +
               (0.2 * result.AvgTopChunkSim);
    }

    public async Task<SemanticSignals> ComputeSemanticSignalsAsync(
        string questionText,
        string answerText,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(questionText) || string.IsNullOrWhiteSpace(answerText))
            return new SemanticSignals(0.0, 0.0, 0.0);

        var q = await _embeddingService.EmbedAsync(questionText.Trim(), ct);
        var a = await _embeddingService.EmbedAsync(answerText.Trim(), ct);

        var simQA = CosineSimilarity(q, a);

        var chunks = SplitIntoChunks(answerText, chunkSize: 40, overlap: 15);
        if (chunks.Count == 0)
            return new SemanticSignals(simQA, simQA, simQA);

        var chunkScores = new List<double>();

        foreach (var chunk in chunks)
        {
            var chunkEmbedding = await _embeddingService.EmbedAsync(chunk, ct);
            var sim = CosineSimilarity(q, chunkEmbedding);
            chunkScores.Add(sim);
        }

        var ordered = chunkScores.OrderByDescending(x => x).ToList();
        var maxChunkSim = ordered[0];
        var avgTopChunkSim = ordered.Take(Math.Min(3, ordered.Count)).Average();

        return new SemanticSignals(simQA, maxChunkSim, avgTopChunkSim);
    }

    public async Task<StarSignals> ComputeStarSignalsAsync(
        string answerText,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(answerText))
            return new StarSignals(0.0, 0.0, 0.0, 0.0, 0.0);

        var chunks = SplitIntoChunks(answerText, chunkSize: 40, overlap: 15);
        if (chunks.Count == 0)
            return new StarSignals(0.0, 0.0, 0.0, 0.0, 0.0);

        var sitProbe = await _embeddingService.EmbedAsync(
            "context background problem situation challenge initial context", ct);

        var taskProbe = await _embeddingService.EmbedAsync(
            "goal responsibility objective task what needed to be done", ct);

        var actionProbe = await _embeddingService.EmbedAsync(
            "actions steps implementation decision what i did how i handled it", ct);

        var resultProbe = await _embeddingService.EmbedAsync(
            "outcome result impact achievement final effect what happened in the end", ct);

        var reflectionProbe = await _embeddingService.EmbedAsync(
            "lesson learned reflection improvement what i learned next time", ct);

        double sitMax = 0.0;
        double taskMax = 0.0;
        double actionMax = 0.0;
        double resultMax = 0.0;
        double reflectionMax = 0.0;

        foreach (var chunk in chunks)
        {
            var chunkEmbedding = await _embeddingService.EmbedAsync(chunk, ct);

            sitMax = Math.Max(sitMax, CosineSimilarity(sitProbe, chunkEmbedding));
            taskMax = Math.Max(taskMax, CosineSimilarity(taskProbe, chunkEmbedding));
            actionMax = Math.Max(actionMax, CosineSimilarity(actionProbe, chunkEmbedding));
            resultMax = Math.Max(resultMax, CosineSimilarity(resultProbe, chunkEmbedding));
            reflectionMax = Math.Max(reflectionMax, CosineSimilarity(reflectionProbe, chunkEmbedding));
        }

        return new StarSignals(sitMax, taskMax, actionMax, resultMax, reflectionMax);
    }

    public async Task<BehavioralFeatureVector> ExtractFeaturesAsync(
        string questionText,
        string answerText,
        CancellationToken ct = default)
    {
        answerText ??= string.Empty;

        var semantic = await ComputeSemanticSignalsAsync(questionText, answerText, ct);
        var star = await ComputeStarSignalsAsync(answerText, ct);

        var words = answerText
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Length;

        var lower = $" {answerText.ToLowerInvariant()} ";
        var numberCount = lower.Count(char.IsDigit);

        string[] actionVerbs =
        {
            "organized", "proposed", "resolved", "led", "coordinated",
            "implemented", "designed", "analyzed", "analysed", "discussed",
            "negotiated", "decided", "planned", "improved", "created",
            "compared", "fixed", "handled", "managed", "supported"
        };

        var actionVerbHits = actionVerbs.Count(v => lower.Contains($" {v} "));
        var hasFirstPerson = lower.Contains(" i ") || lower.Contains(" my ");
        var hasMetrics = numberCount > 0;

        return new BehavioralFeatureVector(
            semantic.SimQA,
            semantic.MaxChunkSim,
            semantic.AvgTopChunkSim,
            star.Situation,
            star.Task,
            star.Action,
            star.Result,
            star.Reflection,
            words,
            actionVerbHits,
            hasFirstPerson ? 1 : 0,
            hasMetrics ? 1 : 0
        );
    }

    public async Task<EvaluationPreview> EvaluatePreviewAsync(
        string questionText,
        string answerText,
        CancellationToken ct = default)
    {
        answerText ??= string.Empty;

        var semantic = await ComputeSemanticSignalsAsync(questionText, answerText, ct);
        var star = await ComputeStarSignalsAsync(answerText, ct);

        var words = answerText
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Length;

        var lower = $" {answerText.ToLowerInvariant()} ";
        var numberCount = lower.Count(char.IsDigit);

        string[] actionVerbs =
        {
            "organized", "proposed", "resolved", "led", "coordinated",
            "implemented", "designed", "analyzed", "analysed", "discussed",
            "negotiated", "decided", "planned", "improved", "created",
            "compared", "fixed", "handled", "managed", "supported"
        };

        var actionVerbHits = actionVerbs.Count(v => lower.Contains($" {v} "));

        var tooShort = words < 40;
        var hasMetrics = numberCount > 0;
        var hasFirstPerson = lower.Contains(" i ") || lower.Contains(" my ");

        var simQaNorm = NormalizeSignal(semantic.SimQA, 0.20, 0.60);
        var maxChunkNorm = NormalizeSignal(semantic.MaxChunkSim, 0.20, 0.60);
        var avgChunkNorm = NormalizeSignal(semantic.AvgTopChunkSim, 0.20, 0.60);

        var sitNorm = NormalizeSignal(star.Situation, 0.20, 0.45);
        var taskNorm = NormalizeSignal(star.Task, 0.20, 0.45);
        var actionNorm = NormalizeSignal(star.Action, 0.20, 0.45);
        var resultNorm = NormalizeSignal(star.Result, 0.20, 0.45);
        var reflectionNorm = NormalizeSignal(star.Reflection, 0.20, 0.45);

        var relevanceScore = ClampTo25(
            (int)Math.Round(
                25.0 * (
                    0.25 * simQaNorm +
                    0.45 * maxChunkNorm +
                    0.30 * avgChunkNorm
                )
            )
        );

        var structureScore = ClampTo25(
            (int)Math.Round(
                25.0 * (
                    0.15 * sitNorm +
                    0.20 * taskNorm +
                    0.35 * actionNorm +
                    0.30 * resultNorm
                )
            )
        );

        var specificityScore =
            (hasMetrics ? 10 : 0) +
            Math.Min(actionVerbHits * 5, 10) +
            (hasFirstPerson ? 5 : 0);

        if (!tooShort && words >= 70)
            specificityScore += 4;

        specificityScore = ClampTo25(specificityScore);

        var impactReflectionScore = ClampTo25(
            (int)Math.Round(
                25.0 * (
                    0.70 * resultNorm +
                    0.30 * reflectionNorm
                )
            ) + (hasMetrics ? 3 : 0)
        );

        var deficits = new List<string>();

        if (semantic.SimQA < 0.28 && semantic.MaxChunkSim < 0.28)
            deficits.Add("OffTopic");

        if (tooShort)
            deficits.Add("TooShort");

        if (star.Situation < 0.24)
            deficits.Add("MissingSituation");

        if (star.Task < 0.24)
            deficits.Add("MissingTask");

        if (star.Action < 0.26)
            deficits.Add("MissingAction");

        if (star.Result < 0.26)
            deficits.Add("MissingResult");

        if (star.Reflection < 0.24)
            deficits.Add("NoReflection");

        if (!hasMetrics && star.Result >= 0.26)
            deficits.Add("MissingMetrics");

        if (!hasFirstPerson)
            deficits.Add("NoOwnership");

        if (actionVerbHits == 0 && !hasMetrics)
            deficits.Add("LowSpecificity");

        var total =
            relevanceScore +
            structureScore +
            specificityScore +
            impactReflectionScore;

        if (tooShort)
            total = (int)Math.Round(total * 0.75);

        total = Math.Clamp(total, 0, 100);

        return new EvaluationPreview(
            total,
            relevanceScore,
            structureScore,
            specificityScore,
            impactReflectionScore,
            semantic,
            star,
            deficits
        );
    }

    private static double NormalizeSignal(double value, double min, double max)
    {
        if (value <= min)
            return 0.0;

        return (value - min) / (max - min);
    }

    private static List<string> SplitIntoChunks(string text, int chunkSize, int overlap)
    {
        var words = text.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var chunks = new List<string>();
        if (words.Length == 0)
            return chunks;

        var step = chunkSize - overlap;
        if (step <= 0)
            step = chunkSize;

        for (var i = 0; i < words.Length; i += step)
        {
            var chunkWords = words.Skip(i).Take(chunkSize).ToArray();
            if (chunkWords.Length == 0)
                break;

            chunks.Add(string.Join(" ", chunkWords));

            if (i + chunkSize >= words.Length)
                break;
        }

        return chunks;
    }

    private static double CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length == 0 || b.Length == 0 || a.Length != b.Length)
            return 0.0;

        double dot = 0.0;
        double normA = 0.0;
        double normB = 0.0;

        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        if (normA == 0.0 || normB == 0.0)
            return 0.0;

        return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
    }
 
    private static int ClampTo25(int value)
    {
        return Math.Clamp(value, 0, 25);
    }
}