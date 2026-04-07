namespace InterSim.Application.Interviews.Sessions.GenerateQuestions;

public static class GenerateQuestionsValidator
{
    public static void Validate(GenerateQuestionsCommand request)
    {
        if (request.SessionId == Guid.Empty)
            throw new InvalidOperationException("SessionId is required.");

        if (string.IsNullOrWhiteSpace(request.Topic))
            throw new InvalidOperationException("Topic is required.");

        if (string.IsNullOrWhiteSpace(request.Difficulty))
            throw new InvalidOperationException("Difficulty is required.");

        if (request.Count <= 0 || request.Count > 20)
            throw new InvalidOperationException("Count must be between 1 and 20.");
    }
}