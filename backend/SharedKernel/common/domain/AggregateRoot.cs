using System.Collections.Immutable;
using SharedKernel.common.domain.entity;
using SharedKernel.common.domain.ids;

namespace SharedKernel.common.domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : IEntityId
{
    protected AggregateRoot() : base() { }

    private readonly List<IDomainEvent> _domainEvents = [];

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

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