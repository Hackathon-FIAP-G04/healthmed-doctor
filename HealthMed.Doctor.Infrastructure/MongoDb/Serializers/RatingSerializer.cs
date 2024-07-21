using HealthMed.Doctor.Core.Domain;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Doctor.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class RatingSerializer : IBsonSerializer<Rating>
    {
        public Type ValueType => typeof(Rating);

        public Rating Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();
            var count = context.Reader.ReadInt32();
            var rating = context.Reader.ReadDecimal128();
            context.Reader.ReadEndDocument();
            return new(count, ((decimal)rating));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Rating value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteInt32("count", value.Count);
            context.Writer.WriteDecimal128("rating", value.Value);
            context.Writer.WriteEndDocument();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Rating rating)
                Serialize(context, args, rating);
            else
                throw new NotSupportedException("Rating is not valid");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
