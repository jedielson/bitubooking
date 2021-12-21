namespace BituBooking.Infra.Storage.Postgres;

using System.Threading;
using System.Threading.Tasks;



using BituBooking.SharedKernell.Context;
using BituBooking.SharedKernell.Domain;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class TransactionManager : ITransactionManager
{
    private readonly BookingContext _context;
    private readonly IMediator _mediator;
    private readonly ILogger<TransactionManager> _logger;

    public TransactionManager(BookingContext context, IMediator mediator, ILogger<TransactionManager> logger)
    {
        _context = context;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task CommitAsync(ResultBase result, CancellationToken ct)
    {
        if (!result.IsSuccess)
        {
            return;
        }

        // _logger.LogDebug("Change tracking before : \n{trackings}", _context.ChangeTracker.DebugView.LongView);
        // _context.ChangeTracker.DetectChanges();
        // _logger.LogDebug("Change tracking after : \n{trackings}", _context.ChangeTracker.DebugView.LongView);
        var domainEvents = new List<IDomainEvent>();
        foreach (var e in _context.ChangeTracker.Entries())
        {
            if (e.Entity is Aggregate a)
            {
                domainEvents.AddRange(a.Events);
            }
        }

        var ok = await _context.SaveChangesAsync(ct);

        if (ok > 0)
        {
            foreach (var e in domainEvents)
            {
                await _mediator.Publish(e, ct);
            }
        }
    }
}
