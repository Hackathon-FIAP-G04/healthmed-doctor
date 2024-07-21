using HealthMed.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Doctor.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class NameSerializer : MongoDB.Bson.Serialization.IBsonSerializer<Name>
    {
        public Type ValueType => typeof(Name);

        public Name Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var name = context.Reader.ReadString();
            return name;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Name value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Name name)
                Serialize(context, args, name);
            else
                throw new NotSupportedException("Name is not valid");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
