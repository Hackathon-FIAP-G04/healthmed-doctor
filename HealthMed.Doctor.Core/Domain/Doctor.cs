using HealthMed.Core.Abstractions;
using HealthMed.Doctor.Core.Domain;

namespace HealthMed.Core.Domain;

public class Doctor : Entity<Id>, IAggregateRoot
{
    public Name Name { get; private set; }
    public CRM CRM { get; private set; }
    public Speciality Speciality { get; private set; }
    public Location Location { get; private set; }
    public Rating Rating { get; private set; }

    public Doctor(string name, string crm, string speciality, double latitude, double longitude)
    {
        Id = Id.New();
        Name = name;
        CRM = crm;
        Speciality = speciality;
        Location = new(latitude, longitude);
        Rating = new(0, 0);
    }

    public void Update(string name, string crm, string speciality, double latitude, double longitude)
    {
        Name = name;
        CRM = crm;
        Speciality = speciality;
        Location = new(latitude, longitude);
    }

    public void Rate(int rating)
    {
        Rating = Rating.Rate(rating);
    }
}
