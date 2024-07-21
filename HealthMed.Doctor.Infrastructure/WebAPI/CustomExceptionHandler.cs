using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HealthMed.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace HealthMed.Infrastructure.WebAPI
{

    public sealed class CustomExceptionHandler
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = context.Response.StatusCode;

            statusCode = GetStatusCodeFromException(exception);

            var problem = new ProblemDetails()
            {
                Type = "https://httpstatuses.com/" + statusCode,
                Title = ((HttpStatusCode)statusCode).ToString(),
                Status = statusCode,
                Detail = exception.Message ?? "An error occurred",
                Instance = context.Request.Path
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(json);
        }

        private static int GetStatusCodeFromException(Exception exception) => exception switch
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
