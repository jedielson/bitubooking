namespace BituBooking.Infra.Kafka;

using BituBooking.Infra.Kafka.Configuration;
using BituBooking.Infra.Storage.Mongo;

using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.Retry;
using KafkaFlow.TypedHandler;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddKafkaServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(ServicesExtensions));

        services.Configure<SourceEventsConfiguration>(configuration.GetSection("KafkaServer.Consumers"));
        services.AddMongoDbServices(configuration);

        services.AddKafka(kafka => kafka
        .UseConsoleLog()
        .AddCluster(cluster =>
        {
            cluster
                .WithBrokers(new[] { configuration.GetValue<string>("KafkaServer:Url") })
                .WithSchemaRegistry(config => config.Url = "localhost:8081");
            cluster.AddKafkaConsumers(configuration);
            cluster.AddKafkaProducers(configuration);
        }
        ));

        return services;
    }

    public static IClusterConfigurationBuilder AddKafkaConsumers(this IClusterConfigurationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("KafkaServer:EnableConsumers"))
        {
            return builder;
        }

        List<SourceEventsConfiguration> consumerSettings = new();
        configuration.GetSection("KafkaServer:Consumers").Bind(consumerSettings);

        foreach (var e in consumerSettings)
        {
            builder.AddConsumer(consumer => consumer
                .Topic(e.TopicName)
                .WithGroupId(e.ConsumerGroupId)
                .WithBufferSize(100)
                .WithWorkersCount(e.ConsumerInstances)
                .AddMiddlewares(m => m
                    .AddSchemaRegistryAvroSerializer()
                    .RetrySimple(cfg => cfg
                        .Handle<Exception>()
                        .TryTimes(5)
                        .WithTimeBetweenTriesPlan(retryCount =>
                            TimeSpan.FromSeconds(Math.Pow(2, retryCount))))
                    .AddTypedHandlers(handlers => handlers
                        .AddHandlersFromAssemblyOf<SourceEventsConfiguration>())
                ));
        }

        return builder;
    }

    public static IClusterConfigurationBuilder AddKafkaProducers(this IClusterConfigurationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("KafkaServer:EnableProducers"))
        {
            return builder;
        }

        var producerSettings = new Dictionary<string, AvroSerializerConfig>();
        configuration.GetSection("KafkaServer:Producers").Bind(producerSettings);

        foreach (var producer in producerSettings)
        {
            builder.AddProducer(producer.Key, p => p
                .DefaultTopic(producer.Key)
                .AddMiddlewares(m => m
                    .AddSchemaRegistryAvroSerializer(producer.Value)));
        }

        return builder;
    }
}
