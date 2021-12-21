namespace BituBooking.SharedKernell.Context;

using FluentResults;

public interface ITransactionManager
{
    Task CommitAsync(ResultBase result, CancellationToken ct);
}
