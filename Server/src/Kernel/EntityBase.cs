using System.ComponentModel.DataAnnotations.Schema;

namespace Kernel;

public abstract class EntityBase<TId>
{
  public TId Id { get; set; } = default!;

  private List<DomainEventBase> _domainEvents = new();
  [NotMapped] public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  internal void ClearDomainEvents() => _domainEvents.Clear();
}
