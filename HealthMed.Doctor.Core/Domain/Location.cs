using static HealthMed.Core.Exceptions;

namespace HealthMed.Core.Domain;

public sealed record Location
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public Location(double latitude, double longitude)
    {
        InvalidLatitudeException.ThrowIf(latitude < -90 || latitude > 90);
        InvalidLongitudeException.ThrowIf(longitude < -180 || longitude > 180);

        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString() => $"({Latitude},{Longitude})";
}
