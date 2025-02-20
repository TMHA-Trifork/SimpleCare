using MediatR;
using SimpleCare.EmergencyWards.Boundary.Events;

namespace SimpleCare.BedWards.Application.EventHandlers;

public class TransferPatientHandler : INotificationHandler<TransferredEvent>
{
    public Task Handle(TransferredEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
