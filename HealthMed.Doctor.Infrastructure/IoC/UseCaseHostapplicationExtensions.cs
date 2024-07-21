using HealthMed.Core.UseCases.RegisterDoctor;
using HealthMed.Core.UseCases.SearchDoctorByLocation;
using HealthMed.Doctor.Core.UseCases.RateDoctor;
using HealthMed.Doctor.Core.UseCases.UpdateDoctor;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class UseCaseHostapplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection builder)
        {
            builder.AddScoped<ISearchDoctorByLocationUseCase, SearchDoctorByLocationUseCase>();
            builder.AddScoped<IRegisterDoctorUseCase, RegisterDoctorUseCase>();
            builder.AddScoped<IUpdateDoctorUseCase, UpdateDoctorUseCase>();
            builder.AddScoped<IRateDoctorUseCase, RateDoctorUseCase>();

            return builder;
        }
    }
}
