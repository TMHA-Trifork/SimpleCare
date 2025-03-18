using MediatR;

using SimpleCare.EmergencyWards.Boundary.Events;
using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.BedWards.Application.EventHandlers;

public class TransferPatientHandler(IOrderly orderlyRoot) : INotificationHandler<EmergencyPatientTransferredEvent>
{
    public async Task Handle(EmergencyPatientTransferredEvent notification, CancellationToken cancellationToken)
    {
        await orderlyRoot.RegisterNewTask(
            "Emergency Unit",
            notification.WardIdentifier,
            $"Transfer patient {notification.PersonalIdentifier} - {notification.GivenNames} {notification.FamilyName}",
            cancellationToken);
    }
}
