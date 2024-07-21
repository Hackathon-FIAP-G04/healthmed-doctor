using static HealthMed.Core.Exceptions;

namespace HealthMed.Doctor.Core.Domain
{
    public sealed record Rating
    {
        public int Count { get; init; } = 0;
        public decimal Value { get; init; } = 0;

        public Rating(int count, decimal value)
        {
            InvalidRatingValueException.ThrowIf(value < 0 || value > 5 || count < 0);

            Count = count;
            Value = value;
        }

        public Rating Rate(int value)
        {
            InvalidRatingValueException.ThrowIf(value < 0 || value > 5);

            var newCount = Count + 1;
            var newRating = (Value*Count + value) / newCount;

            return new Rating(newCount, newRating);
        }

        public override string ToString() => Value.ToString("0.##");

        public static implicit operator decimal(Rating r) => r.Value;

    }
}
