using System.Text.Json;

namespace InterSim.Application.Interviews.Evaluation;

public sealed class BehavioralTrainingExporter
{
    private readonly BehavioralTrainingDataBuilder _builder;

    public BehavioralTrainingExporter(BehavioralTrainingDataBuilder builder)
    {
        _builder = builder;
    }

    public async Task ExportJsonAsync(string path, CancellationToken ct = default)
    {
        var rows = await _builder.BuildAsync(ct);

        var json = JsonSerializer.Serialize(rows, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(path, json, ct);
    }
}