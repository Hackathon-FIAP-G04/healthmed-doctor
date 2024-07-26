using HealthMed.Core.Domain;
using HealthMed.Doctor.Core.Domain;
using HealthMed.Infrastructure.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
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

        public async Task<IEnumerable<HealthMed.Core.Domain.Doctor>> SearchByLocation(Location location, double distance, Speciality? speciality, decimal? rating)
        {
            FilterDefinition<Core.Domain.Doctor> query = Builders<Core.Domain.Doctor>.Filter.Empty;

            if(location != null)
                query &= Builders<HealthMed.Core.Domain.Doctor>
                    .Filter
                    .NearSphere(m => 
                        m.Location, 
                        new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                            new GeoJson2DGeographicCoordinates(location.Latitude, location.Longitude)), distance * 1000, 0.0);

            if (!string.IsNullOrEmpty(speciality))
                query &= Builders<HealthMed.Core.Domain.Doctor>
                    .Filter
                    .Eq(x => x.Speciality, new Speciality(speciality));

            var result = await _doctors.Find(query)
                .ToListAsync();

            if (rating != null)
                result = result.Where(d => d.Rating.Value >= rating).ToList();

            return result;
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
