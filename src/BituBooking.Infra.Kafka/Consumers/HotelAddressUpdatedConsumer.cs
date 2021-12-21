namespace BituBooking.Infra.Kafka.Consumers;

using System.Threading.Tasks;

using BituBooking.Infra.Kafka.Contracts.Events;
using BituBooking.Infra.Storage.Mongo.Documents;
using BituBooking.Infra.Storage.Mongo.Services;

using KafkaFlow;
using KafkaFlow.TypedHandler;

using Microsoft.Extensions.Logging;

public class HotelAddressUpdatedConsumer : IMessageHandler<HotelAddressUpdated>
{
    private readonly IHotelService _service;
    private readonly ILogger<HotelAddressUpdatedConsumer> _logger;

    public HotelAddressUpdatedConsumer(IHotelService service, ILogger<HotelAddressUpdatedConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task Handle(IMessageContext context, HotelAddressUpdated message)
    {
        try
        {
            var _ = (await _service.GetByCodeAsync(message.HotelCode, context.ConsumerContext.WorkerStopped))
            ?? throw new ArgumentNullException($"An hotel with code {message.HotelCode} could not be found.");

            var address = new Storage.Mongo.Documents.Address
            {
                City = message.City,
                Country = message.Country,
                District = message.District,
                Street = message.Street,
                ZipCode = message.ZipCode
            };

            await _service.UpdateAddressAsync(message.HotelCode, address, context.ConsumerContext.WorkerStopped);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing the event: {name}. Code: {code}", nameof(HotelAddressUpdated), message.HotelCode);
            throw;
        }
    }
}
