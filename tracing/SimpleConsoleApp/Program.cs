using System.Diagnostics;
using InstrumentedLibrary;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Simple console app");

using var tracer = OpenTelemetry.Sdk.CreateTracerProviderBuilder()
    .AddSource("*")
    .SetSampler(new AlwaysOnSampler())
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SimpleConsoleApp"))
    .AddHttpClientInstrumentation()
    .AddOtlpExporter()
    .Build();

// Old way of measuring how long stuff takes...
Stopwatch sw = new Stopwatch();
sw.Start();

var result = await new SomeService().DoWorkAsync();
sw.Stop();

Console.WriteLine($"Service call took {sw.ElapsedMilliseconds} ms, and result was '{result}'");
Console.WriteLine("Hit any key to exit");
Console.ReadKey();

tracer.Shutdown();










































