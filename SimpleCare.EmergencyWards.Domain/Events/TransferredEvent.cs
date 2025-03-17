namespace SimpleCare.EmergencyWards.Domain.Events;

public record TransferredEvent(string PersonalIdentifier, string FamilyName, string GivenNames, string WardIdentifier, string Reason);
