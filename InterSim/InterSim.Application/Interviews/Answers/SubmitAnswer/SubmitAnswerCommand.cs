using MediatR;

namespace InterSim.Application.Interviews.Answers.SubmitAnswer;

public sealed record SubmitAnswerCommand(
    Guid SessionId,
    string QuestionText,
    string AnswerText
) : IRequest<Guid>;