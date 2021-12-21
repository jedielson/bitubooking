#nullable disable
namespace BituBooking.Infra.Kafka.Configuration;

public class SourceEventsConfiguration
{
    public string ConsumerGroupId { get; set; }

    public string BrokerList { get; set; }

    public string TopicName { get; set; }

    public IEnumerable<string> EventName { get; set; }

    public string SecurityProtocol { get; set; }

    public string SaslMechanism { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string SslCaLocation { get; set; }

    public int ConsumerInstances { get; set; } = 1;
}
