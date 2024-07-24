using HealthMed.Doctor.Core.Domain;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.Domain;

public class CRMTest
{
    [Fact]
    public void CRMConstructor_ValidValue_ShouldCreateCRM()
    {
        // Arrange
        string validCRMValue = "123456CRM/SP";

        // Act
        var crm = new CRM(validCRMValue);

        // Assert
        Assert.Equal("123456", crm.Numbers);
        Assert.Equal("SP", crm.State);
    }

    [Theory]
    [InlineData("123456CRM/SPX")] // Invalid state
    [InlineData("12345CRM/SP")]   // Number string length < 6
    [InlineData("123456CR/SP")]   // Missing "CRM"
    [InlineData("123456CRM-SP")]  // Missing '/'
    [InlineData("123456CRM")]     // State missing
    public void CRMConstructor_InvalidValue_ShouldThrowInvalidCRMException(string invalidCRMValue)
    {
        // Act & Assert
        Assert.Throws<InvalidCRMException>(() => new CRM(invalidCRMValue));
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        // Arrange
        string validCRMValue = "123456CRM/SP";
        var crm = new CRM(validCRMValue);

        // Act
        string crmString = crm.ToString();

        // Assert
        Assert.Equal(validCRMValue, crmString);
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldReturnCorrectFormat()
    {
        // Arrange
        string validCRMValue = "123456CRM/SP";
        var crm = new CRM(validCRMValue);

        // Act
        string crmString = crm;

        // Assert
        Assert.Equal(validCRMValue, crmString);
    }

    [Fact]
    public void ImplicitConversion_ToCRM_ShouldCreateCRM()
    {
        // Arrange
        string validCRMValue = "123456CRM/SP";

        // Act
        CRM crm = validCRMValue;

        // Assert
        Assert.Equal("123456", crm.Numbers);
        Assert.Equal("SP", crm.State);
    }
}
