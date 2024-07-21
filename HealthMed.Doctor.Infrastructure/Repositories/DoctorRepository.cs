using HealthMed.Core.Domain;
using HealthMed.Doctor.Core.Domain;
using HealthMed.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace HealthMed.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IMongoCollection<HealthMed.Core.Domain.Doctor> _doctors;

        public DoctorRepository(IDbContext context)
        {
            _doctors = context.Database.GetCollection<HealthMed.Core.Domain.Doctor>("doctors");
        }

        public async Task Create(HealthMed.Core.Domain.Doctor doctor)
        {
            await _doctors.InsertOneAsync(doctor);
        }

        public async Task<Core.Domain.Doctor> Get(Id id)
            => await _doctors.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task<Core.Domain.Doctor> GetByCRM(CRM crm)
            => await _doctors.Find(d => d.CRM == crm).FirstOrDefaultAsync();

        public async Task<IEnumerable<HealthMed.Core.Domain.Doctor>> SearchByLocation(Location location, double distance, string? speciality, decimal? rating)
        {
            var query = Builders<HealthMed.Core.Domain.Doctor>.Filter.NearSphere(m => m.Location, location.Latitude, location.Longitude, distance * 1000, 0.0);

            var result = await _doctors.Find(query)
                .ToListAsync();

            return result.Where(d => 
                (string.IsNullOrEmpty(speciality) || d.Speciality == speciality) && 
                (rating == null || d.Rating >= rating));
        }

        public async Task Update(Core.Domain.Doctor doctor)
        {
            var update = Builders<Core.Domain.Doctor>.Update
                .Set(d => d.Name, doctor.Name)
                .Set(d => d.CRM, doctor.CRM)
                .Set(d => d.Rating, doctor.Rating)
                .Set(d => d.Location, doctor.Location)
                .Set(d => d.Speciality, doctor.Speciality);

            await _doctors.UpdateOneAsync(d => d.Id == doctor.Id, update, null);
        }
    }
}
