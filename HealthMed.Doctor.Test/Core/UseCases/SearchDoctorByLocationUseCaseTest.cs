using FluentAssertions;
using HealthMed.Core.Domain;
using HealthMed.Core.UseCases.SearchDoctorByLocation;
using Moq;
using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Test.Core.UseCases
{
    public class SearchDoctorByLocationUseCaseTest
    {
        private readonly Mock<IDoctorRepository> _repositoryMock;
        private readonly SearchDoctorByLocationUseCase _useCase;

        public SearchDoctorByLocationUseCaseTest()
        {
            _repositoryMock = new Mock<IDoctorRepository>();
            _useCase = new SearchDoctorByLocationUseCase(_repositoryMock.Object);
        }

        [Fact]
        public async Task SearchDoctorByLocationAsync_ShouldThrowNotParametersFoundForQueryException_WhenAllParametersAreNull()
        {
            await Assert.ThrowsAsync<NotParametersFoundForQueryException>(() =>
                _useCase.SearchDoctorByLocationAsync(null, null, null, null, null));
        }

        [Theory]
        [InlineData(null, null, 10.0)]
        [InlineData(10.0, null, 10.0)]
        [InlineData(null, 10.0, 10.0)]
        [InlineData(10.0, 10.0, null)]
        [InlineData(10.0, null, null)]
        [InlineData(null, 10.0, null)]
        public async Task SearchDoctorByLocationAsync_ShouldThrowNotAllLocationParametersInformedException_WhenLocationParametersAreInvalid(double? latitude, double? longitude, double? distance)
        {
            await Assert.ThrowsAsync<NotAllLocationParametersInformedException>(() =>
                _useCase.SearchDoctorByLocationAsync(latitude, longitude, distance, null, null));
        }

        [Theory]
        [InlineData(10.0, 20.0, 5.0, "Cardiologist", 4.5)]
        public async Task SearchDoctorByLocationAsync_ShouldReturnDoctorLocationResponse_WhenParametersAreValid(double latitude, double longitude, double distance, string speciality, decimal rating)
        {
            // Arrange
            var doctors = new List<HealthMed.Core.Domain.Doctor> { new HealthMed.Core.Domain.Doctor("Doctor", "123456CRM/RJ", speciality, latitude, longitude) };
            _repositoryMock.Setup(repo => repo.SearchByLocation(It.IsAny<Location>(), It.IsAny<double>(), speciality, rating))
                           .ReturnsAsync(doctors);

            // Act
            var result = await _useCase.SearchDoctorByLocationAsync(latitude, longitude, distance, speciality, rating);

            // Assert
            Assert.NotNull(result);
            result.Doctors.Select(x => x.DoctorId).Should().Contain(doctors[0].Id);
        }
    }
}
