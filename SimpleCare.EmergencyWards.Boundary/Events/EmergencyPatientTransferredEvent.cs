using MediatR;

using SimpleCare.EmergencyWards.Application.Values;

namespace SimpleCare.EmergencyWards.Boundary.Events;

public record EmergencyPatientTransferredEvent(string PersonalIdentifier, string FamilyName, string GivenNames, RecipientWardItem Ward, string Reason) : INotification;
