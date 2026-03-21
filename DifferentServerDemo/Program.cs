var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Root endpoint (fixes your issue)
app.MapGet("/", () => "API is running");

// Your existing endpoints
app.MapGet("/plaintext", () =>
{
    return "Plaintext";
});

app.MapGet("/json", () =>
{
    return new { message = "Hello, World!" };
});

app.MapGet("/data", () =>
{
    var data = Enumerable.Range(1, 100)
        .Select(i => new
        {
            id = i,
            name = $"User {i}",
            email = $"user{i}@example.com"
        });

    return data;
});

app.Run("http://localhost:5000"); //hardcoding address is for kestel only; IIS injects the URLautomatically
//app.Run();