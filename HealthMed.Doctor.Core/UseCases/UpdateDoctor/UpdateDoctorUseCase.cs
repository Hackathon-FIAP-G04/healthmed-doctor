using HealthMed.Core.Domain;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Core.UseCases.UpdateDoctor
{
    public interface IUpdateDoctorUseCase
    {
        Task<UpdateDoctorResponse> UpdateDoctorAsync(UpdateDoctorRequest request);
    }

    public class UpdateDoctorUseCase : IUpdateDoctorUseCase
    {
        private readonly IDoctorRepository _repository;

        public UpdateDoctorUseCase(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateDoctorResponse> UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            var doctor = await _repository.Get(request.Id);

            DoctorNotFoundException.ThrowIfNull(doctor);

            doctor.Update(request.Name, request.CRM, request.Speciality, request.Location.latitude, request.Location.longitude);

            await _repository.Update(doctor);

            return new(doctor);
        }
    }
}
