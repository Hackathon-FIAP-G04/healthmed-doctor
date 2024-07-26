using HealthMed.Core.Domain;

namespace HealthMed.Doctor.Test.Core.Domain;

public class IdTest
{
    [Fact]
    public void New_ShouldCreateNewId()
    {
        // Act
        var id = Id.New();

        // Assert
        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void Undefined_ShouldReturnIdWithEmptyGuid()
    {
        // Act
        var undefinedId = Id.Undefined;

        // Assert
        Assert.Equal(Guid.Empty, undefinedId.Value);
    }

    [Fact]
    public void ImplicitConversion_GuidToId_ShouldCreateId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();

        // Act
        Id id = guid;

        // Assert
        Assert.Equal(guid, id.Value);
    }

    [Fact]
    public void ImplicitConversion_IdToGuid_ShouldReturnGuid()
    {
        // Arrange
        var id = new Id(Guid.NewGuid());

        // Act
        Guid guid = id;

        // Assert
        Assert.Equal(id.Value, guid);
    }

    [Fact]
    public void ImplicitConversion_StringToId_ShouldCreateId()
    {
        // Arrange
        string guidString = Guid.NewGuid().ToString();

        // Act
        Id id = guidString;

        // Assert
        Assert.Equal(Guid.Parse(guidString), id.Value);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrue_WhenIdIsEmpty()
    {
        // Arrange
        var emptyId = Id.Undefined;

        // Act
        bool result = emptyId.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnFalse_WhenIdIsNotEmpty()
    {
        // Arrange
        var id = Id.New();

        // Act
        bool result = id.IsNullOrEmpty();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForEqualIds()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = new Id(guid);
        var id2 = new Id(guid);

        // Act
        bool result = id1.Equals(id2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForEqualGuids()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id = new Id(guid);

        // Act
        bool result = id.Equals(guid);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForEqualStrings()
    {
        // Arrange
        var guidString = Guid.NewGuid();
        var id = new Id(guidString);

        // Act
        bool result = id.Equals(guidString);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturnCorrectHashCode()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id = new Id(guid);

        // Act
        int hashCode = id.GetHashCode();

        // Assert
        Assert.Equal(guid.GetHashCode(), hashCode);
    }
}
