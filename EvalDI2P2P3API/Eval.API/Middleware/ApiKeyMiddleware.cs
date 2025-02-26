namespace Eval.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "x-api-key";
    private readonly IConfiguration _configuration;
    
    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var apiKey = _configuration["ApiSettings:ApiKey"];

        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey) || extractedApiKey != apiKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized: Invalid API Key");
            return;
        }

        await _next(context);
    }
}