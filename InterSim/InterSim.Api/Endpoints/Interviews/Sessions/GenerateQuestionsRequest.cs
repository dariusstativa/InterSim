namespace InterSim.Api.Endpoints.Interviews.Sessions;

public sealed record GenerateQuestionsRequest(
    string Topic,
    string Difficulty,
    int Count
);