namespace BituBooking.SharedKernell.Domain;

using FluentResults;

using MediatR;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResult> : IRequest<TResult>
{

}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
                where TCommand : IRequest<TResult>
{

}