using SimpleCare.EmergencyWards.Interfaces;

namespace SimpleCare.EmergencyWards.Domain;

public class EmergencyWardRoot(IEmergencyPatientRepository patientRepository, IEmergencyEncounterRepository encounterRepository) : IEmergencyWard
{
    public async Task<Encounter> RegisterPatient(string familyName, string givenNames, string observation, CancellationToken cancellationToken)
    {
        var patient = new Patient(Guid.NewGuid(), familyName, givenNames);
        await patientRepository.Add(patient, cancellationToken);

        var encounter = new Encounter(Guid.NewGuid(), patient.PatientId, observation);
        await encounterRepository.Add(encounter, cancellationToken);

        return encounter;
    }
}
