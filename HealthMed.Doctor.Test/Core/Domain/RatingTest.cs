using HealthMed.Doctor.Core.Domain;
using System.Globalization;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.Domain;

public class RatingTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(10, 4.5)]
    [InlineData(1, 5)]
    public void RatingConstructor_ValidValues_ShouldCreateRating(int count, decimal value)
    {
        // Act
        var rating = new Rating(count, value);

        // Assert
        Assert.Equal(count, rating.Count);
        Assert.Equal(value, rating.Value);
    }

    [Theory]
    [InlineData(0, -1)]
    [InlineData(0, 6)]
    [InlineData(-1, 4)]
    public void RatingConstructor_InvalidValues_ShouldThrowInvalidRatingValueException(int count, decimal value)
    {
        // Act & Assert
        Assert.Throws<InvalidRatingValueException>(() => new Rating(count, value));
    }

    [Fact]
    public void Rate_ValidValue_ShouldUpdateRating()
    {
        // Arrange
        var rating = new Rating(2, 4);
        int newValue = 5;

        // Act
        var newRating = rating.Rate(newValue);

        // Assert
        Assert.Equal(3, newRating.Count);
        Assert.Equal(4.33m, newRating.Value, 2);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void Rate_InvalidValue_ShouldThrowInvalidRatingValueException(int invalidValue)
    {
        // Arrange
        var rating = new Rating(2, 4);

        // Act & Assert
        Assert.Throws<InvalidRatingValueException>(() => rating.Rate(invalidValue));
    }

    [Fact]
    public void ImplicitConversion_RatingToDecimal_ShouldReturnValue()
    {
        // Arrange
        var rating = new Rating(2, 4.5m);

        // Act
        decimal value = rating;

        // Assert
        Assert.Equal(4.5m, value);
    }
}
