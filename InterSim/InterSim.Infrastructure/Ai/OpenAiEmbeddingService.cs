using InterSim.Application.Abstractions;
using OpenAI;
using OpenAI.Embeddings;

namespace InterSim.Infrastructure.AI;

public sealed class OpenAiEmbeddingService : IEmbeddingService
{
    private readonly EmbeddingClient _client;

    public OpenAiEmbeddingService()
    {
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("OPENAI_API_KEY is not set.");

        var openAiClient = new OpenAIClient(apiKey);
        _client = openAiClient.GetEmbeddingClient("text-embedding-3-small");
    }

    public async Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
    {
        text = text.Trim();

        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<float>();

        var response = await _client.GenerateEmbeddingAsync(text, cancellationToken: ct);
        return response.Value.ToFloats().ToArray();
    }
}