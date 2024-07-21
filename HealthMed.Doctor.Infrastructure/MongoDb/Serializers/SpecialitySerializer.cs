using HealthMed.Core.Domain;
using HealthMed.Doctor.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Doctor.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class SpecialitySerializer : IBsonSerializer<Speciality>
    {
        public Type ValueType => typeof(Speciality);

        public Speciality Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var speciality = context.Reader.ReadString();
            return speciality;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Speciality value)
        {
            context.Writer.WriteString(value.Value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Speciality speciality)
                Serialize(context, args, speciality);
            else
                throw new NotSupportedException("Speciality is not valid");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
