using HealthMed.Core.Domain;
using HealthMed.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryHostApplicationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection builder) 
        {
            builder.AddScoped<IDoctorRepository, DoctorRepository>();
            return builder;
        }
    }
}
