namespace BituBooking.Infra.Storage.Mongo.Common;

using BituBooking.Infra.Storage.Mongo.Documents;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

public class MongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoContext> _logger;

    public MongoContext(IConfiguration configuration, ILogger<MongoContext> logger)
    {
        _logger = logger;

#pragma warning disable CS0618
        // The correct way is the first way, but it doesn't work till V3
        //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
#pragma warning restore CS0618

        var connectionString = configuration.GetConnectionString("MongoDb");

        var mongoConnectionUrl = new MongoUrl(connectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
        mongoClientSettings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                logger.LogTrace("{commandName} - {command}", e.CommandName, e.Command.ToJson());
            });
        };

        _client = new MongoClient(mongoClientSettings);
        _database = _client.GetDatabase("BituBooking");
    }

    public IMongoCollection<Hotel> Hotels() => _database.GetCollection<Hotel>("Hotels");
}
