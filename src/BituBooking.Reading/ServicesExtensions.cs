namespace BituBooking.Reading;

using BituBooking.SharedKernell.Queries;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddReadingServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServicesExtensions));
        return services;
    }
}
