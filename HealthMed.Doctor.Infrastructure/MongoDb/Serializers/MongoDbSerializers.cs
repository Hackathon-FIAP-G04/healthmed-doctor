using HealthMed.Doctor.Infrastructure.MongoDb.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public static class MongoDbSerializers
    {
        public static void Register()
        {
            BsonSerializer.TryRegisterSerializer(new ObjectSerializer(ObjectSerializer.AllAllowedTypes));
            BsonSerializer.TryRegisterSerializer(GuidSerializer.StandardInstance);
            BsonSerializer.TryRegisterSerializer(new LocationSerializer());
            BsonSerializer.TryRegisterSerializer(new CRMSerializer());
            BsonSerializer.TryRegisterSerializer(new NameSerializer());
            BsonSerializer.TryRegisterSerializer(new RatingSerializer());
            BsonSerializer.TryRegisterSerializer(new SpecialitySerializer());
        }
    }
}
