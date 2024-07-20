using HealthMed.Core.Abstractions;

namespace HealthMed.Core.Domain
{
    public class Doctor : Entity<Id>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string CRM { get; private set; }
        public string Speciality { get; private set; }
        public Location Location { get; private set; }

        public Doctor(string name, string crm, string speciality, double latitude, double longitude)
        {
            Id = Id.New();
            Name = name;
            CRM = crm;
            Location = new(latitude, longitude);
        }
    }
}
