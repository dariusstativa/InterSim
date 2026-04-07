using System;
using System.Collections.Generic;
using System.Linq;

namespace InterSim.Application.Interviews.Evaluation;

public static class BaselineFeedbackSelectorV1
{
    public static IReadOnlyList<FeedbackItem> Select(
        int score,
        IReadOnlyList<DeficitTag> deficits,
        QuestionType questionType)
    {
        if (questionType != QuestionType.BehavioralStar)
            return Array.Empty<FeedbackItem>();

        var maxItems = DetermineMaxItems(score);

        
        var orderedDeficits = OrderBySeverity(deficits);

        var selected = new List<FeedbackItem>();
        var coveredCategories = new Dictionary<string, int>();

        foreach (var deficit in orderedDeficits)
        {
            if (selected.Count >= maxItems)
                break;

            var category = GetCategory(deficit);

          
            if (coveredCategories.TryGetValue(category, out var count) && count >= 2)
                continue;

            var candidates = FeedbackLibraryV1.GetByDeficit(deficit);
            if (candidates.Count == 0)
                continue;

           
            var item = candidates[0];

            selected.Add(item);

            if (!coveredCategories.ContainsKey(category))
                coveredCategories[category] = 0;

            coveredCategories[category]++;
        }

        return selected;
    }

    private static int DetermineMaxItems(int score)
    {
        if (score >= 80) return 3;
        if (score >= 50) return 4;
        return 5;
    }

    private static IReadOnlyList<DeficitTag> OrderBySeverity(IReadOnlyList<DeficitTag> deficits)
    {
        return deficits
            .OrderBy(GetSeverity)
            .ToList();
    }

    private static int GetSeverity(DeficitTag tag)
    {
        return tag switch
        {
            
            DeficitTag.OffTopic => 1,
            DeficitTag.MissingResult => 1,
            DeficitTag.MissingAction => 1,
            DeficitTag.LowStructure => 1,
            DeficitTag.TooShort => 1,

           
            DeficitTag.MissingTask => 2,
            DeficitTag.LowSpecificity => 2,
            DeficitTag.MissingMetrics => 2,
            DeficitTag.NoOwnership => 2,

            
            DeficitTag.VagueOutcome => 3,
            DeficitTag.NoReflection => 3,
            DeficitTag.TooLong => 3,

            _ => 3
        };
    }

    private static string GetCategory(DeficitTag tag)
    {
        return tag switch
        {
            DeficitTag.MissingTask or
            DeficitTag.MissingAction or
            DeficitTag.MissingResult or
            DeficitTag.LowStructure
                => "Structure",

            DeficitTag.LowSpecificity or
            DeficitTag.MissingMetrics or
            DeficitTag.NoOwnership
                => "Specificity",

            DeficitTag.VagueOutcome or
            DeficitTag.NoReflection
                => "Impact",

            DeficitTag.TooShort or
            DeficitTag.TooLong
                => "Length",

            DeficitTag.OffTopic
                => "Relevance",

            _ => "Other"
        };
    }
}