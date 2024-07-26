using HealthMed.Core.Abstractions;

namespace HealthMed.Core.Domain;

public class AvailableSchedule : Entity<Id>, IAggregateRoot
{
    public Id DoctorId { get; private set; }
    public DayOfWeek[] DaysOfWeek { get; private set; }
    public int StartHour { get; private set; }
    public int EndHour { get; private set; }
}
