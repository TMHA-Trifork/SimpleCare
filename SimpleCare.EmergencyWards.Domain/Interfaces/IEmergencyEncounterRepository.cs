namespace SimpleCare.EmergencyWards.Domain.Interfaces;

public interface IEmergencyEncounterRepository
{
    Task Add(Encounter encounter, CancellationToken cancellationToken);
}
