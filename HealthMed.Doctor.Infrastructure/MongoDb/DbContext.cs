using HealthMed.Core.Domain;
using HealthMed.Infrastructure.MongoDb.Configurations;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using System.Reflection.Metadata;

namespace HealthMed.Infrastructure.MongoDb
{
    public interface IDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }

    public class DbContext : IDbContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public DbContext(MongoDbConfiguration configuration)
        {
            var url = new MongoUrl(configuration.ConnectionString);
            var settings = MongoClientSettings.FromUrl(url);
            var options = new InstrumentationOptions { CaptureCommandText = true };

            settings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber(options));

            Client = new MongoClient(settings);
            Database = Client.GetDatabase(configuration.Database);
            
            Database.GetCollection<Doctor>("doctors").Indexes.CreateOne(new CreateIndexModel<Doctor>(Builders<Doctor>.IndexKeys.Geo2DSphere(d => d.Location)));
        }
    }
}