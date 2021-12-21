namespace BituBooking.SharedKernell.Domain;

using MediatR;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : INotification
{

}
