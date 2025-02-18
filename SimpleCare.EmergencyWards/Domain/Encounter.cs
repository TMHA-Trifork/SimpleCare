namespace SimpleCare.EmergencyWards.Domain;

public record Encounter(Guid Id, Guid PatientId, string EncounterReason);
