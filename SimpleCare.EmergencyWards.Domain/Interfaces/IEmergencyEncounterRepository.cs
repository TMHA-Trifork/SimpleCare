using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Interfaces;

public interface IEmergencyEncounterRepository
{
    Task Add(Encounter encounter, CancellationToken cancellationToken);
}
