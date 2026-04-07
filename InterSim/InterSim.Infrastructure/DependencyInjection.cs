using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;
using InterSim.Infrastructure.AI;
using InterSim.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterSim.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Db")));

        services.AddScoped<IInterviewSessionRepository, InterviewSessionRepository>();
        services.AddScoped<IQuestionBankRepository, QuestionBankRepository>();
        services.AddScoped<ISessionQuestionRepository, SessionQuestionRepository>();
        services.AddScoped<ISessionReader, SessionReader>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IFeedbackEngine, HeuristicFeedbackEngineV1>();

        services.AddSingleton<IEmbeddingService, OpenAiEmbeddingService>();
        services.AddScoped<BehavioralEvaluatorV2>();
        services.AddHttpClient<BehavioralDatasetGenerationService>();

        return services;
    }
}