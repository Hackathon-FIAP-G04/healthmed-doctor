using HealthMed.Core.UseCases.RegisterDoctor;

namespace HealthMed.Doctor.Core.UseCases.UpdateDoctor
{
    public class UpdateDoctorResponse : RegisterDoctorResponse
    {
        public UpdateDoctorResponse(HealthMed.Core. Domain.Doctor doctor) : base(doctor)
        {
            
        }
    }
}
