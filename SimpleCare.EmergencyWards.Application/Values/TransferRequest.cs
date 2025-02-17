namespace SimpleCare.EmergencyWards.Application.Values;

public record TransferRequest(Guid patientId, string familyName, string givenNames, string wardIdentifier);
