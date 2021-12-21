namespace BituBooking.Infra.Kafka.Producers;

using System.Threading;

using BituBooking.Domain.Management.Events;
using BituBooking.SharedKernell.Domain;

using KafkaFlow.Producers;

public class RoomAddedHandler : IEventHandler<RoomAdded>
{
    private readonly IProducerAccessor _endpoint;

    public RoomAddedHandler(IProducerAccessor endpoint)
    {
        _endpoint = endpoint;
    }

    public Task Handle(RoomAdded notification, CancellationToken cancellationToken)
    {
        var @event = new Contracts.Events.RoomAdded
        {
            Amenities = notification.Amenities?.ToList(),
            AvailableQuantity = notification.AvailableQuantity,
            Capacity = notification.Capacity,
            Code = notification.Code,
            Description = notification.Description,
            HotelCode = notification.HotelCode,
            Name = notification.Name,
            PricePerNight = notification.PricePerNight.ToString()
        };

        return _endpoint[Constants.Topics.RoomAdded].ProduceAsync(Guid.NewGuid().ToString(), @event);
    }
}
