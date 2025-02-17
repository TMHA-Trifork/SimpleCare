namespace SimpleCare.EmergencyWards.Domain;

public record Encounter(Guid EncounterId, Guid PatientId, string EncounterReason);
