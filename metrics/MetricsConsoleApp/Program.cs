

using System.Diagnostics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using System.Diagnostics.Metrics;

const int MemoryThreshold = 1024 * 1024 * 500; // 500 Megabytes

Console.WriteLine("Hello, World!");

using var metrics = OpenTelemetry.Sdk.CreateMeterProviderBuilder()
    .AddMeter("*")
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(nameof(Program)))
    .AddConsoleExporter()
    .Build();

Meter myMeter = new("Identec");
ObservableGauge<long> gauge = myMeter.CreateObservableGauge<long>("MemoryUsage", () =>
{
    return Process.GetProcesses()
        .Where(p=> p.WorkingSet64 > MemoryThreshold)
        .Select(p => new Measurement<long>(
            p.WorkingSet64,
            new KeyValuePair<string, object?>("process.name", p.ProcessName)));
});

Console.WriteLine("Press any key to exit");
Console.ReadKey();
