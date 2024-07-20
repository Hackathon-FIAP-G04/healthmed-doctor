namespace HealthMed.Core.UseCases.RegisterDoctor
{
    public class RegisterDoctorRequest
    {
        public string Name { get; set; }
        public string CRM { get; set; }
        public string Speciality { get; set; }
        public RegisterDoctorLocationRequest Location { get; set; }
    }

    public record RegisterDoctorLocationRequest(double latitude, double longitude);
}
