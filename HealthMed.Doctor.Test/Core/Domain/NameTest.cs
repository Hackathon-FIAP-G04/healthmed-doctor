using HealthMed.Core.Domain;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.Domain;

public class NameTests
{
    [Fact]
    public void NameConstructor_ValidValue_ShouldCreateName()
    {
        // Arrange
        string validName = "John Doe";

        // Act
        var name = new Name(validName);

        // Assert
        Assert.Equal(validName, name.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NameConstructor_InvalidValue_ShouldThrowInvalidNameException(string invalidName)
    {
        // Act & Assert
        Assert.Throws<InvalidNameException>(() => new Name(invalidName));
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        string validName = "John Doe";
        var name = new Name(validName);

        // Act
        string nameString = name.ToString();

        // Assert
        Assert.Equal(validName, nameString);
    }

    [Fact]
    public void ImplicitConversion_StringToName_ShouldCreateName()
    {
        // Arrange
        string validName = "John Doe";

        // Act
        Name name = validName;

        // Assert
        Assert.Equal(validName, name.Value);
    }

    [Fact]
    public void ImplicitConversion_NameToString_ShouldReturnValue()
    {
        // Arrange
        var name = new Name("John Doe");

        // Act
        string nameString = name;

        // Assert
        Assert.Equal("John Doe", nameString);
    }
}
