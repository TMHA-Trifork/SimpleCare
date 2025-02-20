using SimpleCare.EmergencyWards.Boundary.Events;
using SimpleCare.EmergencyWards.Domain.Events;

namespace SimpleCare.EmergencyWards.Application.Mappers;

internal static class EmergencyEventsMapper
{
    public static EmergencyPatientTransferredEvent Map(TransferredEvent transferredEvent)
    {
        return new EmergencyPatientTransferredEvent(
            transferredEvent.PersonalIdentifier,
            transferredEvent.FamilyName,
            transferredEvent.GivenNames,
            transferredEvent.WardIdentifier,
            transferredEvent.Reason);
    }
}
