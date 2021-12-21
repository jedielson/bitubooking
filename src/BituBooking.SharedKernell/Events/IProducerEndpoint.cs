namespace BituBooking.SharedKernell.Events;

public interface IProducerEndpoint
{
    Task Produce<TEvent>(TEvent @event, string topicName) where TEvent : class;
}
