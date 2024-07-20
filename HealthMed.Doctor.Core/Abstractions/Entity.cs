﻿using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Core.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity<TId> : IEntity
    where TId : class
    {
        private ICollection<IDomainEvent> events = new List<IDomainEvent>();

        public TId Id { get; init; }

        public IReadOnlyCollection<IDomainEvent> Events => events.ToList().AsReadOnly();

        public void ClearEvents() => events.Clear();

        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            if (events is null)
            {
                events = new List<IDomainEvent>();
            }

            events.Add(domainEvent);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId> other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (GetType() != other.GetType()) return false;

            if (Id.Equals(default) || other.Id.Equals(default)) return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> first, Entity<TId> second)
        {
            if (first is null && second is null) return true;

            if (first is null || second is null) return false;

            return first.Equals(second);
        }

        public static bool operator !=(Entity<TId> first, Entity<TId> second) => !(first == second);

        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
    }
}
