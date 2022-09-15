using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace InstrumentedLibrary;

public class SomeService
{
    public static ActivitySource Source = new ActivitySource("MyActivitySource");

    public async Task<string> DoWorkAsync()
    {
        // Simulate workload
        await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(3, 5)));
        var client = new HttpClient();
        await client.GetAsync("https://www.identecsolutions.com");

        return "Everything is ok!";
    }

}