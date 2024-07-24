using HealthMed.Core.Domain;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.Domain;

public class LocationTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(45.0, 90.0)]
    [InlineData(-45.0, -90.0)]
    [InlineData(89.9999, 179.9999)]
    [InlineData(-89.9999, -179.9999)]
    public void LocationConstructor_ValidValues_ShouldCreateLocation(double latitude, double longitude)
    {
        // Act
        var location = new Location(latitude, longitude);

        // Assert
        Assert.Equal(latitude, location.Latitude);
        Assert.Equal(longitude, location.Longitude);
    }

    [Theory]
    [InlineData(-91.0, 0)]
    [InlineData(91.0, 0)]
    public void LocationConstructor_InvalidLatitude_ShouldThrowInvalidLatitudeException(double invalidLatitude, double longitude)
    {
        // Act & Assert
        Assert.Throws<InvalidLatitudeException>(() => new Location(invalidLatitude, longitude));
    }

    [Theory]
    [InlineData(0, -181.0)]
    [InlineData(0, 181.0)]
    public void LocationConstructor_InvalidLongitude_ShouldThrowInvalidLongitudeException(double latitude, double invalidLongitude)
    {
        // Act & Assert
        Assert.Throws<InvalidLongitudeException>(() => new Location(latitude, invalidLongitude));
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        // Arrange
        var location = new Location(45.0, 90.0);

        // Act
        string locationString = location.ToString();

        // Assert
        Assert.Equal("(45,90)", locationString);
    }
}
