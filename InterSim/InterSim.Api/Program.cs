using System.Text.Json.Serialization;
using InterSim.Api.Endpoints.Interviews;
using InterSim.Api.Endpoints.Interviews.Answers;
using InterSim.Api.Endpoints.Interviews.Evaluation;
using InterSim.Api.Endpoints.Interviews.Sessions;
using InterSim.Api.Middleware;
using InterSim.Application;
using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;
using InterSim.Infrastructure;
using InterSim.Infrastructure.AI;
using InterSim.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<IFollowUpQuestionGenerator, OpenAiFollowUpQuestionGenerator>();
builder.Services.AddHttpClient<ILlmFollowUpTrigger, OpenAiLlmFollowUpTrigger>();

builder.Services.AddScoped<BehavioralEvaluatorV2>();
builder.Services.AddScoped<BehavioralTrainingDataBuilder>();
builder.Services.AddScoped<BehavioralTrainingExporter>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<IFollowUpTrigger, RuleBasedFollowUpTrigger>();

builder.Services.AddScoped<IAppDbContext>(sp =>
    sp.GetRequiredService<AppDbContext>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("Frontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCreateSessionEndpoint();
app.MapGenerateQuestionsEndpoint();
app.MapGetSessionEndpoint();
app.MapSubmitAnswerEndpoint();
app.MapCompleteSessionEndpoint();
app.MapGetAnswersBySession();
app.MapEvaluateBehavioral();
app.MapEvaluateWithFollowUpLlm();
app.MapEvaluateWithFollowUpRule();

app.Run();