namespace BituBooking.Infra.Kafka.Consumers;

using System.Threading.Tasks;

using BituBooking.Infra.Kafka.Contracts.Events;
using BituBooking.Infra.Storage.Mongo.Documents;
using BituBooking.Infra.Storage.Mongo.Services;

using KafkaFlow;
using KafkaFlow.TypedHandler;

using Microsoft.Extensions.Logging;

public class RoomAddedConsumer : IMessageHandler<RoomAdded>
{
    private readonly IHotelService _service;
    private readonly ILogger<HotelAddressUpdatedConsumer> _logger;

    public RoomAddedConsumer(IHotelService service, ILogger<HotelAddressUpdatedConsumer> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task Handle(IMessageContext context, RoomAdded message)
    {
        try
        {
            _ = (await _service.GetByCodeAsync(message.HotelCode, context.ConsumerContext.WorkerStopped))
           ?? throw new ArgumentNullException($"An hotel with code {message.HotelCode} could not be found.");

            _ = decimal.TryParse(message.PricePerNight, out var price);

            var room = new Room
            {
                Code = message.Code,
                AvailableQuantity = message.AvailableQuantity,
                Amenities = message.Amenities?.ToList() ?? new(),
                Capacity = message.Capacity,
                Description = message.Description,
                Name = message.Name,
                PricePerNight = price
            };

            await _service.AddRoomAsync(message.HotelCode, room, context.ConsumerContext.WorkerStopped);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing the event: {name}. Code: {code}", nameof(HotelAddressUpdated), message.HotelCode);
            throw;
        }
    }
}
