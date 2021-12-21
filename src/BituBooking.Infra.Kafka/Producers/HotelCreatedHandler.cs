namespace BituBooking.Infra.Kafka.Producers;

using System.Threading;

using BituBooking.Domain.Management.Events;
using BituBooking.SharedKernell.Domain;

using KafkaFlow.Producers;

public class HotelCreatedHandler : IEventHandler<HotelCreated>
{
    private readonly IProducerAccessor _endpoint;

    public HotelCreatedHandler(IProducerAccessor endpoint)
    {
        _endpoint = endpoint;
    }

    public Task Handle(HotelCreated @event, CancellationToken cancellationToken)
    {
        var mapped = new Contracts.Events.HotelCreated
        {
            Code = @event.Code,
            Address = new Contracts.Events.Address
            {
                City = @event.Address.City,
                Country = @event.Address.Country,
                District = @event.Address.District,
                Street = @event.Address.Street,
                ZipCode = @event.Address.ZipCode
            },
            Contacts = new Contracts.Events.Contacts
            {
                Email = @event.Contacts.Email,
                Mobile = @event.Contacts.Mobile,
                Phone = @event.Contacts.Phone
            },
            Name = @event.Name,
            StarsOfCategory = @event.StarsOfCategory,
            StarsOfRating = @event.StarsOfRating
        };

        return _endpoint[Constants.Topics.HotelCreated].ProduceAsync(Guid.NewGuid().ToString(), mapped);
    }
}
