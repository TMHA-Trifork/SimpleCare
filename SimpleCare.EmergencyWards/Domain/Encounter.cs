using SimpleCare.EmergencyWards.Interfaces;

namespace SimpleCare.EmergencyWards.Domain;

public record Encounter(Guid Id, Guid PatientId, string EncounterReason)
{
    internal static async Task<Encounter> CreateNew(Guid patientId, string observation, IEmergencyEncounterRepository encounterRepository, CancellationToken cancellationToken)
    {
        var encounter = new Encounter(Guid.NewGuid(), patientId, observation);
        await encounterRepository.Add(encounter, cancellationToken);

        return encounter;
    }
}
