namespace BituBooking.Infra.Kafka.Consumers;

using System.Threading.Tasks;

using BituBooking.Infra.Kafka.Contracts.Events;
using BituBooking.Infra.Storage.Mongo.Documents;
using BituBooking.Infra.Storage.Mongo.Services;

using KafkaFlow;
using KafkaFlow.TypedHandler;

using Microsoft.Extensions.Logging;

public class HotelCreatedConsumer : IMessageHandler<HotelCreated>
{
    private readonly IHotelService _service;
    private readonly ILogger<HotelCreatedConsumer> _logger;

    public HotelCreatedConsumer(IHotelService service, ILogger<HotelCreatedConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task Handle(IMessageContext context, HotelCreated message)
    {
        _logger.LogDebug("\n\n\nConsumed message in {name}\n\n\n", nameof(HotelCreatedConsumer));

        Hotel h = new();
        h.Code = message.Code;
        h.Name = message.Name;
        h.StarsOfCategory = message.StarsOfCategory;
        h.StarsOfRating = message.StarsOfRating;

        h.Address = new Storage.Mongo.Documents.Address
        {
            City = message.Address.City,
            Country = message.Address.Country,
            District = message.Address.District,
            Street = message.Address.Street,
            ZipCode = message.Address.ZipCode
        };

        h.Contacts = new Storage.Mongo.Documents.Contacts
        {
            Email = message.Contacts.Email,
            Mobile = message.Contacts.Mobile,
            Phone = message.Contacts.Phone
        };

        await _service.Save(h, context.ConsumerContext.WorkerStopped);
    }
}
