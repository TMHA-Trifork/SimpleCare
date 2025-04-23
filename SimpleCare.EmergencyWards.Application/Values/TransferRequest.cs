namespace SimpleCare.EmergencyWards.Application.Values;

public record TransferRequest(Guid PatientId, string FamilyName, string GivenNames, Guid WardId, string Reason);
