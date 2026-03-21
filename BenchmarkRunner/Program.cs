using System.Diagnostics;

var url = "http://localhost:5000/data"; // change to 44300 for IIS
int totalRequests = 10_000;
int concurrency = 50;

var httpClient = new HttpClient();

var sw = Stopwatch.StartNew();

var tasks = new List<Task>();

int requestsPerTask = totalRequests / concurrency;

/*for (int i = 0; i < concurrency; i++)
{
    tasks.Add(Task.Run(async () =>
    {
        for (int j = 0; j < requestsPerTask; j++)
        {
            await httpClient.GetAsync(url);
        }
    }));
}*/

int success = 0;
int failed = 0;

for (int j = 0; j < requestsPerTask; j++)
{
    try
    {
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
            Interlocked.Increment(ref success);
        else
            Interlocked.Increment(ref failed);
    }
    catch
    {
        Interlocked.Increment(ref failed);
    }
}

await Task.WhenAll(tasks);

sw.Stop();

var rps = totalRequests / sw.Elapsed.TotalSeconds;

Console.WriteLine($"Total Requests: {totalRequests}");
Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds:F2}s");
Console.WriteLine($"Requests/sec: {rps:F0}");