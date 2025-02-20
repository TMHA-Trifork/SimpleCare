using SimpleCare.EmergencyWards.Boundary.Events;
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
        var patient = new Patient(Guid.NewGuid(), familyName, givenNames, EmergencyPatientStatus.Registered);
        await patientRepository.Add(patient, cancellationToken);

        var encounter = new Encounter(Guid.NewGuid(), patient.Id, observation);
        await encounterRepository.Add(encounter, cancellationToken);

        return encounter;
    }

    public async Task<TransferredEvent> TransferPatient(Guid patientId, string familyName, string givenNames, string wardIdentifier, CancellationToken cancellationToken)
    {
        var patient = (await patientRepository.Get(patientId, cancellationToken))
            ?? throw new InvalidOperationException($"Patient with ID {patientId} not found.");

        patient = patient.Transfer(wardIdentifier);

        patientRepository.Update(patient, cancellationToken);

        return new TransferredEvent(patientId, familyName, givenNames, wardIdentifier);
    }
}
