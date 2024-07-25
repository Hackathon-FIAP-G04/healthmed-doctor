using HealthMed.Core.Abstractions;

namespace HealthMed.Core
{
    public static class Exceptions
    {
        public class InvalidLatitudeException(): DomainException<InvalidLatitudeException>("Latitude must be a number between -90 and 90");
        public class InvalidLongitudeException() : DomainException<InvalidLongitudeException>("Longitude must be a number between -180 and 180");
        public class InvalidNameException() : DomainException<InvalidNameException>("Provided name is invalid");
        public class InvalidCRMException() : DomainException<InvalidCRMException>("Provided CRM is invalid");
        public class InvalidSpecialityException() : DomainException<InvalidSpecialityException>("Speciality is not valid");
        public class InvalidRatingValueException() : DomainException<InvalidRatingValueException>("The rating value must be between 0 and 5");
        public class DoctorNotFoundException() : DomainException<DoctorNotFoundException>("Requested doctor was not found");
        public class NotAllLocationParametersInformedException() : DomainException<NotAllLocationParametersInformedException>("Either every location parameter must be provider or none");
        public class NotParametersFoundForQueryException() : DomainException<NotParametersFoundForQueryException>("At least one query parameter is required");
    }
}
