using HealthMed.Infrastructure.MongoDb.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using HealthMed.Infrastructure.MongoDb.Serializers;

namespace HealthMed.Infrastructure.MongoDb.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoDbHostApplicationExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection builder, IConfiguration configuration)
        {
            var mongoDbConfiguration = configuration
                .GetRequiredSection("MongoDb")
                .Get<MongoDbConfiguration>();

            mongoDbConfiguration!.ThrowIfInvalid();

            builder.AddSingleton(mongoDbConfiguration);
            builder.AddSingleton<IDbContext, DbContext>();

            MongoDbSerializers.Register();
            MongoDbConventions.Register();

            return builder;
        }
    }
}
