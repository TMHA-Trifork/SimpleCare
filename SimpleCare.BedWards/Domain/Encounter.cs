namespace SimpleCare.BedWards.Domain;

public enum EncounterStatus
{
    Admitted,
    Discharged
};

public record Encounter(Guid Id, Guid PatientId, EncounterStatus Status);
