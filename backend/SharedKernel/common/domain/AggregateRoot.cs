using System.Collections.Immutable;
using SharedKernel.common.domain.entity;
using SharedKernel.common.domain.ids;

namespace SharedKernel.common.domain;

public abstract class AggregateRoot<IdType> : Entity<IdType>, IAggregateRoot where IdType : IEntityId
{
    protected AggregateRoot() : base() { }

    protected readonly List<IDomainEvent> _domainEvents = new();

    public IImmutableList<IDomainEvent> GetDomainEventsCopy() => _domainEvents.ToImmutableList();

    public List<IDomainEvent> PopAndClearDomainEvents() {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}

public interface IAggregateRoot
{
    public List<IDomainEvent> PopAndClearDomainEvents();
}