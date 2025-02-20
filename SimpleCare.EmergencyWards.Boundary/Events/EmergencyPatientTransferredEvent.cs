using MediatR;

namespace SimpleCare.EmergencyWards.Boundary.Events;

public record EmergencyPatientTransferredEvent(string PersonalIdentifier, string FamilyName, string GivenNames, string WardIdentifier, string Reason) : INotification;
