namespace BituBooking.Infra.Storage.Postgres;

using BituBooking.Domain.Management;
using BituBooking.Infra.Storage.Postgres.Repositories;
using BituBooking.SharedKernell.Context;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddPostgresServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });

        services.AddScoped<IManagementRepository, ManagementRepository>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddMediatR(typeof(ServicesExtensions));
        return services;
    }
}
