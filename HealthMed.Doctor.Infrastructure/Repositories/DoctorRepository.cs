using HealthMed.Core.Domain;
using HealthMed.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace HealthMed.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IMongoCollection<Doctor> _doctors;

        public DoctorRepository(IDbContext context)
        {
            _doctors = context.Database.GetCollection<Doctor>("doctors");
        }

        public async Task Create(Doctor doctor)
        {
            await _doctors.InsertOneAsync(doctor);
        }

        public async Task<IEnumerable<Doctor>> SearchByLocation(Location location, double distance, string speciality)
        {
            var query = Builders<Doctor>.Filter.NearSphere(m => m.Location, location.Latitude, location.Longitude, distance * 1000, 0.0);

            var result = await _doctors.Aggregate()
                .Match(d => d.Speciality == speciality)
                .Match(query)
                .ToListAsync();

            return result;
        }
    }
}
