using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.API.Host.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails;

            if (exception is ValidationException validationException)
            {
                // Thrown by ValidationBehavior when a MediatR request fails validation.
                _logger.LogWarning(exception, "Validation failed: {Message}", exception.Message);

                problemDetails = new ValidationProblemDetails(
                    validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()))
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed"
                };
            }
            else
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error",
                    // Do not leak internal exception messages outside Development.
                    Detail = _environment.IsDevelopment() ? exception.Message : null
                };
            }

            httpContext.Response.StatusCode = problemDetails.Status!.Value;

            // Pass the runtime type so ValidationProblemDetails.Errors is serialized too.
            await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), cancellationToken);

            return true; // Return true to indicate the exception was handled
        }
    }
}
