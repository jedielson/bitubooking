namespace BituBooking.Domain;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServicesExtensions));
        return services;
    }
}
