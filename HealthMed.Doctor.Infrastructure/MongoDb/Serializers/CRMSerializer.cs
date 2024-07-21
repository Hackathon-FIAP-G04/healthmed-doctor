using HealthMed.Core.Domain;
using HealthMed.Doctor.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Doctor.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class CRMSerializer : MongoDB.Bson.Serialization.IBsonSerializer<CRM>
    {
        public Type ValueType => typeof(CRM);

        public CRM Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var crm = context.Reader.ReadString();
            return crm;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CRM value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is CRM crm)
                Serialize(context, args, crm);
            else
                throw new NotSupportedException("Name is not valid");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
