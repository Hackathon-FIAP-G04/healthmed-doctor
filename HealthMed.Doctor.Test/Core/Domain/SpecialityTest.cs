using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.Domain;

public class SpecialityTests
{
    [Fact]
    public void SpecialityConstructor_ValidValue_ShouldCreateSpeciality()
    {
        // Arrange
        string validSpeciality = "Cardiology";
        var availableSpecialities = new[] { "Cardiology", "Dermatology", "Neurology" };

        // Act
        var speciality = new Speciality(validSpeciality, availableSpecialities);

        // Assert
        Assert.Equal(validSpeciality, speciality.Value);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        string validSpeciality = "Cardiology";
        var availableSpecialities = new[] { "Cardiology", "Dermatology", "Neurology" };
        var speciality = new Speciality(validSpeciality, availableSpecialities);

        // Act
        string result = speciality.ToString();

        // Assert
        Assert.Equal(validSpeciality, result);
    }

    [Fact]
    public void ImplicitConversion_StringToSpeciality_ShouldCreateSpeciality()
    {
        // Arrange
        string validSpeciality = "Cardiology";
        var availableSpecialities = new[] { "Cardiology", "Dermatology", "Neurology" };

        // Act
        Speciality speciality = new Speciality(validSpeciality, availableSpecialities);

        // Assert
        Assert.Equal(validSpeciality, speciality.Value);
    }

    [Fact]
    public void ImplicitConversion_SpecialityToString_ShouldReturnValue()
    {
        // Arrange
        var availableSpecialities = new[] { "Cardiology", "Dermatology", "Neurology" };
        var speciality = new Speciality("Cardiology", availableSpecialities);

        // Act
        string value = speciality;

        // Assert
        Assert.Equal("Cardiology", value);
    }
}

public sealed record Speciality
{
    public string Value { get; }

    public Speciality(string value, string[] availableSpecialities)
    {
        InvalidSpecialityException.ThrowIfNull(
            availableSpecialities.Length > 0 && !availableSpecialities.Contains(value)
        );

        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Speciality speciality) => speciality.Value;

    public static implicit operator Speciality(string value) => new Speciality(value, Array.Empty<string>());
}
