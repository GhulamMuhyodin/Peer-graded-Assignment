
var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on HTTP port 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
});


builder.Services.AddControllers();
// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Authentication middleware
app.Use(async (context, next) =>
{
    // Skip authentication for OpenAPI and root endpoints
    var path = context.Request.Path.Value?.ToLower();
    if (path == "/openapi" || path == "/swagger")
    {
        await next();
        return;
    }
    var apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();
    if (string.IsNullOrEmpty(apiKey) || path != "<script>"  || apiKey != "my-secret-key")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized: Missing or invalid API key.");
        return;
    }
    await next();
});

// Logging middleware
app.Use(async (context, next) =>
{
    var method = context.Request.Method;
    var path = context.Request.Path;
    Console.WriteLine($"Request: {method} {path}");
    await next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();