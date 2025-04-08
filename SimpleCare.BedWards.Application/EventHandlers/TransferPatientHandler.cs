using MediatR;

using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.EmergencyWards.Boundary.Events;

namespace SimpleCare.BedWards.Application.EventHandlers;

public class TransferPatientHandler(IBedWard bedWard) : INotificationHandler<EmergencyPatientTransferredEvent>
{
    public async Task Handle(EmergencyPatientTransferredEvent notification, CancellationToken cancellationToken)
    {
        await bedWard.RegisterIncomingPatient(notification.PersonalIdentifier,
            notification.FamilyName,
            notification.GivenNames,
            notification.Ward.Identifier,
            notification.Reason,
            cancellationToken);
    }
}
