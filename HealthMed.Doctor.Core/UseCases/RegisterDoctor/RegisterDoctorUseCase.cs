using HealthMed.Core.Domain;

namespace HealthMed.Core.UseCases.RegisterDoctor
{
    public interface IRegisterDoctorUseCase
    {
        Task<RegisterDoctorResponse> RegisterDoctorAsync(RegisterDoctorRequest request);
    }
    public class RegisterDoctorUseCase : IRegisterDoctorUseCase
    {
        private readonly IDoctorRepository _repository;

        public RegisterDoctorUseCase(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterDoctorResponse> RegisterDoctorAsync(RegisterDoctorRequest request)
        {
            var doctor = new Domain.Doctor(request.Name, request.CRM, request.Speciality, request.Location.latitude, request.Location.longitude);

            await _repository.Create(doctor);

            return new(doctor);
        }
    }
}
