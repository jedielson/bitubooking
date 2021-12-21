namespace BituBooking.Infra.Storage.Mongo;

using BituBooking.Infra.Storage.Mongo.Common;
using BituBooking.Infra.Storage.Mongo.Services;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(ServicesExtensions));
        services.AddSingleton<MongoContext>();
        services.AddSingleton<IHotelService, HotelService>();
        return services;
    }
}
