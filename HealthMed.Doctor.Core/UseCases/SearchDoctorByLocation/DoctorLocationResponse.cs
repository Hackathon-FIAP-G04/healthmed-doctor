using HealthMed.Core.Domain;

namespace HealthMed.Core.UseCases.SearchDoctorByLocation
{
    public class DoctorLocationResponse
    {
        public IEnumerable<DoctorLocationResponseItem> Doctors { get; set; }

        public DoctorLocationResponse(IEnumerable<Doctor> doctors)
        {
            Doctors = doctors.Select(x => new DoctorLocationResponseItem(x.Id, x.CRM, x.Name, new LocationResponse(x.Location.Latitude, x.Location.Longitude))).ToList();
        }
    }
    public record LocationResponse(double Latitude, double Longitude);

    public record DoctorLocationResponseItem(Guid DoctorId, string CRM, string Name, LocationResponse Location);
}
