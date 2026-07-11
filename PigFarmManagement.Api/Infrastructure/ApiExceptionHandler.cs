using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PigFarmManagement.Api.Infrastructure;

/// <summary>
/// Converts expected application exceptions into consistent HTTP problem details.
/// </summary>
public sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            InvalidOperationException => (StatusCodes.Status409Conflict, "Operation cannot be completed"),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        logger.LogError(exception, "Request {TraceId} failed with status code {StatusCode}.",
            httpContext.TraceIdentifier, statusCode);

        await Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: statusCode < StatusCodes.Status500InternalServerError ? exception.Message : null)
            .ExecuteAsync(httpContext);

        return true;
    }
}
