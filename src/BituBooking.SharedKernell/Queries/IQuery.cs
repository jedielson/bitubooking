namespace BituBooking.SharedKernell.Queries;

using MediatR;

public interface IQuery<TQuery, TResult> : IRequest<TResult>
    where TResult : class?
{
}

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TResult : class?
    where TQuery : IRequest<TResult>
{

}