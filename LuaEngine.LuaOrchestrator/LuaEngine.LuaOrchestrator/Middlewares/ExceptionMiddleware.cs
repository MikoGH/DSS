using System.Net;

namespace LuaEngine.LuaOrchestrator.Middlewares;

/// <summary>
/// Промежуточное ПО для перехвата и обработки исключений.
/// </summary>
public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, "httpContext");
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Items.TryAdd("LoggedException", ex);
            _logger.LogError(new EventId(ex.HResult), ex, ex.ToString());
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        _logger.LogError(ex, "Exception occured");
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        return WriteJsonContentAsync(httpContext, statusCode, ex.Message);
    }

    private static Task WriteJsonContentAsync<TError>(HttpContext httpContext, HttpStatusCode statusCode, TError error)
    {
        httpContext.Response.StatusCode = (int)statusCode;
        return httpContext.Response.WriteAsJsonAsync(error);
    }
}
