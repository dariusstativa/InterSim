using System.Collections.Generic;

namespace InterSim.Application.Interviews.Evaluation;

public static class FeedbackLibraryV1
{
    
    private static readonly IReadOnlyDictionary<DeficitTag, IReadOnlyList<FeedbackItem>> ItemsByDeficit
        = new Dictionary<DeficitTag, IReadOnlyList<FeedbackItem>>
        {
            [DeficitTag.MissingTask] = new List<FeedbackItem>
            {
                new("MissingTask_ClarifyResponsibility", "Clearly state what your specific responsibility was in that situation."),
                new("MissingTask_StateObjective", "Explicitly mention the goal or problem you were trying to solve."),
                new("MissingTask_Expectation", "Clarify what was expected from you in that context."),
            },

            [DeficitTag.MissingAction] = new List<FeedbackItem>
            {
                new("MissingAction_StepByStep", "Describe what you did, step by step."),
                new("MissingAction_Decisions", "Avoid general statements—explain the decisions you personally made."),
                new("MissingAction_Initiative", "Highlight your initiative, not only what the team did."),
            },

            [DeficitTag.MissingResult] = new List<FeedbackItem>
            {
                new("MissingResult_StateOutcome", "Add a clear outcome of your actions."),
                new("MissingResult_Ending", "Explain how the situation ended."),
                new("MissingResult_ConcreteImpact", "State the concrete impact of the solution you applied."),
            },

            [DeficitTag.LowStructure] = new List<FeedbackItem>
            {
                new("LowStructure_Order", "Organize your answer logically: context → actions → result."),
                new("LowStructure_STAR", "Use a clear STAR structure to improve coherence."),
                new("LowStructure_SeparateParts", "Separate the situation from the actions you took more clearly."),
            },

            [DeficitTag.LowSpecificity] = new List<FeedbackItem>
            {
                new("LowSpecificity_AddDetails", "Add concrete details to avoid vague wording."),
                new("LowSpecificity_SpecificExamples", "Provide specific examples, not just general descriptions."),
                new("LowSpecificity_PreciseContext", "Include more precise technical or contextual details."),
            },

            [DeficitTag.MissingMetrics] = new List<FeedbackItem>
            {
                new("MissingMetrics_MeasurableOutcome", "Include a measurable outcome (%, time, cost, performance)."),
                new("MissingMetrics_AddNumbers", "Add numbers to show the real impact of your actions."),
                new("MissingMetrics_QuantifyResult", "Quantify the result to make it more convincing."),
            },

            [DeficitTag.NoOwnership] = new List<FeedbackItem>
            {
                new("NoOwnership_UseI", "Clarify your personal contribution using first-person statements (\"I did...\")."),
                new("NoOwnership_AvoidOnlyWe", "Avoid only saying \"we\"—specify what you personally contributed."),
                new("NoOwnership_DirectContribution", "Highlight your direct contribution to the result."),
            },

            [DeficitTag.VagueOutcome] = new List<FeedbackItem>
            {
                new("VagueOutcome_TooGeneral", "Your outcome is too general—explain exactly what improved."),
                new("VagueOutcome_ConcreteEffects", "Describe the concrete effects of your solution."),
                new("VagueOutcome_AvoidGeneric", "Avoid generic conclusions like \"it went well\" without details."),
            },

            [DeficitTag.NoReflection] = new List<FeedbackItem>
            {
                new("NoReflection_Learnings", "Add what you learned from this experience."),
                new("NoReflection_WhatDifferent", "Mention what you would do differently next time."),
                new("NoReflection_PersonalReflection", "Include a short personal reflection about the situation."),
            },

            [DeficitTag.OffTopic] = new List<FeedbackItem>
            {
                new("OffTopic_AddressQuestion", "Your answer doesn’t directly address the question—refocus on what was asked."),
                new("OffTopic_LinkToPrompt", "Connect your story more clearly to the exact prompt."),
                new("OffTopic_DirectAnswer", "Make sure your answer directly responds to the question."),
            },

            [DeficitTag.TooShort] = new List<FeedbackItem>
            {
                new("TooShort_ExpandSteps", "Your answer is too short—expand each stage of the story."),
                new("TooShort_AddActionsResults", "Add more details about your actions and the results."),
                new("TooShort_AddContext", "Provide more context so the story feels complete."),
            },

            [DeficitTag.TooLong] = new List<FeedbackItem>
            {
                new("TooLong_BeConcise", "Your answer is too long—try to make it more concise."),
                new("TooLong_RemoveSecondary", "Remove secondary details that don’t add value."),
                new("TooLong_FocusEssential", "Focus on the essential information that answers the question."),
            },
        };

    public static IReadOnlyList<FeedbackItem> GetByDeficit(DeficitTag deficit)
        => ItemsByDeficit.TryGetValue(deficit, out var items)
            ? items
            : Empty;

    private static readonly IReadOnlyList<FeedbackItem> Empty = new List<FeedbackItem>();
}