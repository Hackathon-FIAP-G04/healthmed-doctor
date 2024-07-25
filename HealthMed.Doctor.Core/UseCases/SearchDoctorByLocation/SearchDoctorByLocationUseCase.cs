using HealthMed.Core.Domain;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Core.UseCases.SearchDoctorByLocation
{
    public interface ISearchDoctorByLocationUseCase
    {
        Task<DoctorLocationResponse> SearchDoctorByLocationAsync(double? latitude, double? longitude, double? distance, string? speciality, decimal? rating);
    }

    public class SearchDoctorByLocationUseCase : ISearchDoctorByLocationUseCase
    {
        private readonly IDoctorRepository _repository;

        public SearchDoctorByLocationUseCase(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorLocationResponse> SearchDoctorByLocationAsync(double? latitude, double? longitude, double? distance, string? speciality, decimal? rating)
        {
            NotParametersFoundForQueryException.ThrowIf(ValidateEveryParameterIsNull(latitude, longitude, distance, speciality, rating));

            NotAllLocationParametersInformedException.ThrowIf(ValidateLocationParametersInvalid(latitude, longitude, distance));

            var location = (latitude == null || longitude == null) ? null : new Location(latitude.Value, longitude.Value);

            var doctors = await _repository.SearchByLocation(location, distance.HasValue ? distance.Value : 0.0, speciality, rating);

            return new(doctors);
        }

        private bool ValidateLocationParametersInvalid(double? latitude, double? longitude, double? distance)
        {
            if(latitude == null && longitude == null && distance == null) return false;
            else if(latitude == null || longitude == null || distance == null) return true;
            return false;
        }

        private bool ValidateEveryParameterIsNull(double? latitude, double? longitude, double? distance, string? speciality, decimal? rating)
            => latitude == null &&
               longitude == null &&
               distance == null &&
               speciality == null &&
               rating == null;
    }
}
