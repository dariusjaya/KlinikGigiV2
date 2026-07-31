namespace KlinikGigiV2.SharedKernel;

public interface IHasDomainEvents
{
  IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
  void RegisterDomainEvent(DomainEventBase domainEvent);
  void ClearDomainEvents();
}
