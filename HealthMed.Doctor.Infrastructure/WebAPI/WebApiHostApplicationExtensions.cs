using HealthMed.Infrastructure.IoC;
using HealthMed.Infrastructure.MongoDb.Extensions;
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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRepositories();
            builder.Services.AddUseCases();
            builder.Services.AddMongoDb(builder.Configuration);
            builder.Services.AddHealthChecks();

            return builder;
        }

        public static WebApplication UseWebApi(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware(typeof(CustomExceptionHandler));
            app.UseHttpsRedirection();
            app.MapControllers();
            app.MapHealthChecks("/hc");

            return app;
        }
    } 
}
