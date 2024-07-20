namespace HealthMed.Core.Domain
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> SearchByLocation(Location location, double distance, string speciality);

        Task Create(Doctor doctor);
    }
}
