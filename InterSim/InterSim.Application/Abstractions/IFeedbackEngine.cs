namespace InterSim.Application.Interviews.Evaluation;

public interface IFeedbackEngine
{
    AnswerFeedbackResult Generate(
        string questionText,
        string answerText,
        QuestionType questionType);
}