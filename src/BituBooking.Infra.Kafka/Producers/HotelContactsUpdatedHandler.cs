namespace BituBooking.Infra.Kafka.Producers;

using System.Threading;

using BituBooking.Domain.Management.Events;
using BituBooking.SharedKernell.Domain;

using KafkaFlow.Producers;

public class HotelContactsUpdatedHandler : IEventHandler<HotelContactsUpdated>
{
    private readonly IProducerAccessor _endpoint;

    public HotelContactsUpdatedHandler(IProducerAccessor endpoint)
    {
        _endpoint = endpoint;
    }

    public Task Handle(HotelContactsUpdated notification, CancellationToken cancellationToken)
    {
        var @event = new Contracts.Events.HotelContactsUpdated
        {
            Email = notification.Email,
            HotelCode = notification.HotelCode,
            Mobile = notification.Mobile,
            Phone = notification.Phone
        };

        return _endpoint[Constants.Topics.HotelContactsUpdated].ProduceAsync(Guid.NewGuid().ToString(), @event);
    }
}
