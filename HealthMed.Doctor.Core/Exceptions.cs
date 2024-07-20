using HealthMed.Core.Abstractions;

namespace HealthMed.Core
{
    public static class Exceptions
    {
        public class InvalidLatitudeException(): DomainException<InvalidLatitudeException>("Latitude must be a number between -90 and 90");
        public class InvalidLongitudeException() : DomainException<InvalidLongitudeException>("Longitude must be a number between -180 and 180");

    }
}
