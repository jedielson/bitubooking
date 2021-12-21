namespace BituBooking.Infra.Kafka.Consumers;

using System.Threading.Tasks;

using BituBooking.Infra.Kafka.Contracts.Events;
using BituBooking.Infra.Storage.Mongo.Documents;
using BituBooking.Infra.Storage.Mongo.Services;

using KafkaFlow;
using KafkaFlow.TypedHandler;

using Microsoft.Extensions.Logging;

public class HotelContactsUpdatedConsumer : IMessageHandler<HotelContactsUpdated>
{
    private readonly IHotelService _service;
    private readonly ILogger<HotelContactsUpdatedConsumer> _logger;

    public HotelContactsUpdatedConsumer(IHotelService service, ILogger<HotelContactsUpdatedConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }
    public async Task Handle(IMessageContext context, HotelContactsUpdated message)
    {
        _logger.LogWarning("Running {name} consumer", nameof(HotelAddressUpdatedConsumer));
        try
        {
            var _ = (await _service.GetByCodeAsync(message.HotelCode, context.ConsumerContext.WorkerStopped))
            ?? throw new ArgumentNullException($"An hotel with code {message.HotelCode} could not be found.");

            var contacts = new Storage.Mongo.Documents.Contacts
            {
                Email = message.Email,
                Mobile = message.Mobile,
                Phone = message.Phone
            };

            await _service.UpdateContactsAsync(message.HotelCode, contacts, context.ConsumerContext.WorkerStopped);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing the event: {name}. Code: {code}", nameof(HotelAddressUpdated), message.HotelCode);
            throw;
        }
    }
}
