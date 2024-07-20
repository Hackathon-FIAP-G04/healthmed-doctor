namespace HealthMed.Infrastructure.MongoDb.Models
{
    public class LocationObject
    {
        public string Type { get; set; }
        public double[] Coordinates { get; set; }

        public LocationObject(double latitude, double longitude)
        {
            Type = "Point";
            Coordinates = [latitude, longitude];
        }
    }
}
