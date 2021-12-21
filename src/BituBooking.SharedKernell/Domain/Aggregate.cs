namespace BituBooking.SharedKernell.Domain;

public class Aggregate : Entity
{
    public Aggregate(Guid? identifier)
        : base(identifier)
    {
    }

    //
    private readonly List<IDomainEvent> _events = new();

    public IReadOnlyList<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}

