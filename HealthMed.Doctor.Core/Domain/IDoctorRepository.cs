using HealthMed.Doctor.Core.Domain;

namespace HealthMed.Core.Domain
{
    public interface IDoctorRepository
    {
        Task<Doctor> Get(Id id);
        Task<Doctor> GetByCRM(CRM crm);
        Task<IEnumerable<Doctor>> SearchByLocation(Location location, double distance, string? speciality, decimal? rating);

        Task Create(Doctor doctor);
        Task Update(Doctor doctor);
    }
}
