namespace BituBooking.SharedKernell.Queries;

using MediatR;

public static class QueryExtensions
{
    public static Task<TResult> DispatchQuery<TResult>(this IMediator mediator, IRequest<TResult> query, CancellationToken ct)
    {
        return mediator.Send(query, ct);
    }
}
