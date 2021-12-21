namespace BituBooking.Infra.Kafka.Producers;

using System.Threading;

using BituBooking.Domain.Management.Events;
using BituBooking.SharedKernell.Domain;

using KafkaFlow.Producers;

public class HotelAddressUpdatedHandler : IEventHandler<HotelAddressUpdated>
{
    private readonly IProducerAccessor _endpoint;

    public HotelAddressUpdatedHandler(IProducerAccessor endpoint)
    {
        _endpoint = endpoint;
    }

    public Task Handle(HotelAddressUpdated notification, CancellationToken cancellationToken)
    {
        var @event = new Contracts.Events.HotelAddressUpdated
        {
            City = notification.City,
            Country = notification.Country,
            District = notification.District,
            HotelCode = notification.HotelCode,
            Street = notification.Street,
            ZipCode = notification.ZipCode
        };

        return _endpoint[Constants.Topics.HotelAddressUpdated].ProduceAsync(Guid.NewGuid().ToString(), @event);
    }
}
