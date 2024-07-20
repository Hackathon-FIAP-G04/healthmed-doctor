using HealthMed.Core.UseCases.SearchDoctorByLocation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class UseCaseHostapplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection builder)
        {
            builder.AddScoped<ISearchDoctorByLocationUseCase, SearchDoctorByLocationUseCase>();

            return builder;
        }
    }
}
