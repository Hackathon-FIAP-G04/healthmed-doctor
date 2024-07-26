using DoctorEntity = HealthMed.Core.Domain.Doctor;

namespace HealthMed.Doctor.Test.Core.Domain;

public class DoctorTest
{
    [Fact]
    public void DoctorConstructor_ValidValues_ShouldCreateDoctor()
    {
        // Arrange
        string name = "John Doe";
        string crm = "123456CRM/SP";
        string speciality = "Cardiology";
        double latitude = -23.5505;
        double longitude = -46.6333;

        // Act
        var doctor = new DoctorEntity(name, crm, speciality, latitude, longitude);

        // Assert
        Assert.NotNull(doctor.Id);
        Assert.Equal(name, doctor.Name);
        Assert.Equal(crm, doctor.CRM);
        Assert.Equal(speciality, doctor.Speciality);
        Assert.Equal(latitude, doctor.Location.Latitude);
        Assert.Equal(longitude, doctor.Location.Longitude);
        Assert.Equal(0, doctor.Rating.Value);
        Assert.Equal(0, doctor.Rating.Count);
    }

    [Fact]
    public void Update_ValidValues_ShouldUpdateDoctorDetails()
    {
        // Arrange
        var doctor = new DoctorEntity("John Doe", "123456CRM/SP", "Cardiology", -23.5505, -46.6333);
        string newName = "Jane Doe";
        string newCrm = "654321CRM/RJ";
        string newSpeciality = "Dermatology";
        double newLatitude = -22.9068;
        double newLongitude = -43.1729;

        // Act
        doctor.Update(newName, newCrm, newSpeciality, newLatitude, newLongitude);

        // Assert
        Assert.Equal(newName, doctor.Name);
        Assert.Equal(newCrm, doctor.CRM);
        Assert.Equal(newSpeciality, doctor.Speciality);
        Assert.Equal(newLatitude, doctor.Location.Latitude);
        Assert.Equal(newLongitude, doctor.Location.Longitude);
    }

    [Fact]
    public void Rate_ValidRating_ShouldUpdateRating()
    {
        // Arrange
        var doctor = new DoctorEntity("John Doe", "123456CRM/SP", "Cardiology", -23.5505, -46.6333);
        int rating = 4;

        // Act
        doctor.Rate(rating);

        // Assert
        Assert.Equal(4, doctor.Rating.Value);
        Assert.Equal(1, doctor.Rating.Count);
    }
}
