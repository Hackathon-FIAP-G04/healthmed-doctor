using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static HealthMed.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.WebAPI
{
    
    public sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContent httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {exceptionMessage}, Ocurred at: {time}",
                exception.Message, DateTime.UtcNow);

            int statusCode = GetStatusCodeFromException(exception);

            /*ProblemDetails problemDetails = new()
            {
                Type = "https://httpstatuses.com/" + statusCode,
                Title = exception.GetType().Name,
                Detail = exception.Message,
                Status = statusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add(new KeyValuePair<string, object?>("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier));

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            */
            return true;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private int GetStatusCodeFromException(Exception exception) => exception switch
        {
            InvalidLatitudeException => StatusCodes.Status400BadRequest,
            InvalidLongitudeException => StatusCodes.Status400BadRequest,
            InvalidNameException => StatusCodes.Status400BadRequest,
            InvalidCRMException => StatusCodes.Status400BadRequest,
            InvalidSpecialityException => StatusCodes.Status400BadRequest,
            InvalidRatingValueException => StatusCodes.Status400BadRequest,
            DoctorNotFoundException => StatusCodes.Status404NotFound,

            _ => StatusCodes.Status500InternalServerError,
        };
    }
}
