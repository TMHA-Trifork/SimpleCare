using SimpleCare.EmergencyWards.Interfaces;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Domain;

public class EmergencyWardRoot(IEmergencyPatientRepository patientRepository, IEmergencyEncounterRepository encounterRepository) : IEmergencyWard
{
    public Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        return patientRepository.Get(patientId, cancellationToken);
    }

    public Task<ImmutableList<Patient>> GetPatients(CancellationToken cancellationToken)
    {
        return patientRepository.GetAll(cancellationToken);
    }

    public async Task<Encounter> RegisterPatient(string familyName, string givenNames, string observation, CancellationToken cancellationToken)
    {
        var patient = new Patient(Guid.NewGuid(), familyName, givenNames);
        await patientRepository.Add(patient, cancellationToken);

        var encounter = new Encounter(Guid.NewGuid(), patient.Id, observation);
        await encounterRepository.Add(encounter, cancellationToken);

        return encounter;
    }
}
