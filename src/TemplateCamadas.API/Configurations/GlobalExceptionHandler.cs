using Microsoft.AspNetCore.Diagnostics;
using TemplateCamadas.Domain.Models.Responses;

namespace TemplateCamadas.API.Configurations;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        var message = _environment.IsDevelopment() ? exception.Message : "An unexpected error occurred.";

        var response = new ResponseBase
        {
            Result = new Result
            {
                Success = false,
                Messages = new List<string> { message }
            }
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
