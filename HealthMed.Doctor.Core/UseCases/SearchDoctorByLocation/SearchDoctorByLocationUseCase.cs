using HealthMed.Core.Domain;

namespace HealthMed.Core.UseCases.SearchDoctorByLocation
{
    public interface ISearchDoctorByLocationUseCase
    {
        Task<DoctorLocationResponse> SearchDoctorByLocationAsync(double latitude, double longitude, double distance, string speciality);
    }

    public class SearchDoctorByLocationUseCase : ISearchDoctorByLocationUseCase
    {
        private readonly IDoctorRepository _repository;

        public SearchDoctorByLocationUseCase(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorLocationResponse> SearchDoctorByLocationAsync(double latitude, double longitude, double distance, string speciality)
        {
            var doctors = await _repository.SearchByLocation(new Location(latitude, longitude), distance, speciality);

            return new(doctors);
        }
    }
}
