using HealthMed.Core.Domain;

namespace HealthMed.Doctor.Core.UseCases.RateDoctor
{
    public interface IRateDoctorUseCase
    {
        Task<RateDoctorResponse> RateDoctor(RateDoctorRequest request);
    }

    public class RateDoctorUseCase : IRateDoctorUseCase
    {
        private readonly IDoctorRepository _repository;

        public RateDoctorUseCase(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<RateDoctorResponse> RateDoctor(RateDoctorRequest request)
        {
            var doctor = await _repository.GetByCRM(request.CRM);

            doctor.Rate(request.Rating);

            await _repository.Update(doctor);

            return new()
            {
                CRM = doctor.CRM,
                Rating = doctor.Rating.ToString()
            };
        }
    }
}
