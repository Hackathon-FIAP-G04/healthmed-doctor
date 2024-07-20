using HealthMed.Core.Domain;
using HealthMed.Infrastructure.MongoDb.Models;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace HealthMed.Infrastructure.MongoDb.Serializers
{
    [ExcludeFromCodeCoverage]
    public class LocationSerializer : IBsonSerializer<Location>
    {
        public Type ValueType => typeof(Location);

        public Location Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();
            context.Reader.ReadString("type");
            context.Reader.ReadStartArray();
            var latitude = context.Reader.ReadDouble();
            var longitude = context.Reader.ReadDouble();
            context.Reader.ReadEndArray();
            context.Reader.ReadEndDocument();
            return new Location(latitude, longitude);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Location value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteString("type", "Point");
            context.Writer.WriteStartArray("coordinates");
            context.Writer.WriteDouble(value.Latitude);
            context.Writer.WriteDouble(value.Longitude);
            context.Writer.WriteEndArray();
            context.Writer.WriteEndDocument();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Location location)
                Serialize(context, args, location);
            else
                throw new NotSupportedException("This location is not valid");
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
