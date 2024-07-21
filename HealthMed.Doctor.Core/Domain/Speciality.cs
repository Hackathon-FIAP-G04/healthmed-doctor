using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Core.Domain
{
    public sealed record Speciality
    {
        public string Value { get; }

        public Speciality(string value)
        {
            InvalidSpecialityException.ThrowIfNull(_availableSpecialities.Length > 0 && !_availableSpecialities.Contains(value));

            Value = value;
        }

        public override string ToString() => Value;

        public static implicit operator string(Speciality speciality) => speciality.Value;

        public static implicit operator Speciality(string value) => new Speciality(value);

        private readonly string[] _availableSpecialities = [];
    }
}
