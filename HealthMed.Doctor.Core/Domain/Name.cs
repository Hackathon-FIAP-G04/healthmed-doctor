using static HealthMed.Core.Exceptions;

namespace HealthMed.Core.Domain;

public sealed record Name
{
    public string Value { get; }

    public Name(string name)
    {
        InvalidNameException.ThrowIf(string.IsNullOrEmpty(name));

        Value = name;
    }

    public override string ToString() => Value;

    public static implicit operator string(Name name) => name.Value;

    public static implicit operator Name(string value) => new(value);
}
