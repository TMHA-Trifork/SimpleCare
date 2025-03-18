namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWardEncounterRepository
{
    Task<Encounter?> GetActiveEncounterByPatientId(Guid patientId, CancellationToken cancellationToken);

    Task AddEncounter(Encounter encounter, CancellationToken cancellationToken);
}
