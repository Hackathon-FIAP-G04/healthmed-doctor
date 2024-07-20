using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.WebAPI
{
    [ExcludeFromCodeCoverage]
    public static class WebApiHostApplicationExtensions
    {
        public static IHostApplicationBuilder AddWebApi(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .AllowAnyHeader();
                });
            });

            return builder;
        }

        public static WebApplication UseWebApi(this WebApplication app)
        {
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.MapControllers();

            return app;
        }
    } 
}
