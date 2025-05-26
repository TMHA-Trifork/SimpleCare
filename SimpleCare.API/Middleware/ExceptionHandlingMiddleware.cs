using Microsoft.AspNetCore.Mvc;

namespace SimpleCare.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOperationException ex)
        {
            string exceptionMessage = BuildErrorMessage(ex);

            logger.LogInformation(exceptionMessage);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Status = context.Response.StatusCode,
                Detail = exceptionMessage
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred while processing the request.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Status = context.Response.StatusCode,
                Detail = "An unhandled exception occurred."
            });
        }
    }

    private static string BuildErrorMessage(Exception exception)
    {
        var logentry = exception.Message;
        while (exception.InnerException != null)
        {
            exception = exception.InnerException;
            logentry += $" -> {exception.Message}";
        }

        return logentry;
    }
}
