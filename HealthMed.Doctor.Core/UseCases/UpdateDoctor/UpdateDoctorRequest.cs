using HealthMed.Core.UseCases.RegisterDoctor;

namespace HealthMed.Doctor.Core.UseCases.UpdateDoctor
{
    public class UpdateDoctorRequest : RegisterDoctorRequest
    {
        public Guid Id { get; set; }
    }
}
