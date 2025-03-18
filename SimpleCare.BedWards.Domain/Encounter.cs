using SimpleCare.BedWards.Domain.Interfaces;

namespace SimpleCare.BedWards.Domain;

public enum EncounterStatus
{
    Admitted,
    Discharged
};

public record Encounter(Guid Id, Guid PatientId, EncounterStatus Status)
{
    internal static async Task<bool> HasActiveForPatientId(Guid patientId, IBedWardEncounterRepository bedWardEncounterRepository, CancellationToken cancellationToken)
    {
        var encounter = await bedWardEncounterRepository.GetActiveEncounterByPatientId(patientId, cancellationToken);
        return encounter is not null;
    }

    internal static async Task AddNewAdmittance(Guid patientId, IBedWardEncounterRepository bedWardEncounterRepository, CancellationToken cancellationToken)
    {
        var encounter = new Encounter(Guid.NewGuid(), patientId, EncounterStatus.Admitted);
        await bedWardEncounterRepository.AddEncounter(encounter, cancellationToken);
    }
}
