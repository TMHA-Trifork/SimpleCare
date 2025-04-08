using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Boundary.Events;
using SimpleCare.EmergencyWards.Domain.Events;

namespace SimpleCare.EmergencyWards.Application.Mappers;

internal static class EmergencyEventsMapper
{
    public static EmergencyPatientTransferredEvent MapWith(this TransferredEvent transferredEvent, BedWardItem ward)
    {
        return new EmergencyPatientTransferredEvent(
            transferredEvent.PersonalIdentifier,
            transferredEvent.FamilyName,
            transferredEvent.GivenNames,
            new RecipientWardItem(
                ward.Id,
                ward.Identifier,
                ward.Name),
            transferredEvent.Reason);
    }
}
