using HealthMed.Core.Domain;

namespace HealthMed.Core.UseCases.RegisterDoctor
{
    public class RegisterDoctorResponse : RegisterDoctorRequest
    {
        public Guid Id { get; set; }

        public RegisterDoctorResponse(HealthMed.Core.Domain.Doctor doctor)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            CRM = doctor.CRM;
            Speciality = doctor.Speciality;
            Location = new(doctor.Location.Latitude,doctor.Location.Longitude);
        }
    }
}
