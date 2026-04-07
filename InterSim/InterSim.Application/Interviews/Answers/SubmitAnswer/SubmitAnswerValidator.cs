namespace InterSim.Application.Interviews.Answers.SubmitAnswer;

public static class SubmitAnswerValidator
{
    public static void Validate(SubmitAnswerCommand request)
    {
        if (request.SessionId == Guid.Empty)
            throw new InvalidOperationException("SessionId is required.");

        if (string.IsNullOrWhiteSpace(request.QuestionText))
            throw new InvalidOperationException("QuestionText is required.");

        if (string.IsNullOrWhiteSpace(request.AnswerText))
            throw new InvalidOperationException("AnswerText is required.");
    }
}