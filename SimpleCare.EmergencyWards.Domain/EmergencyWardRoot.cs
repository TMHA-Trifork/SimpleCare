using SimpleCare.EmergencyWards.Domain.Events;
using SimpleCare.EmergencyWards.Interfaces;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Domain;

public class EmergencyWardRoot(IEmergencyPatientRepository patientRepository, IEmergencyEncounterRepository encounterRepository) : IEmergencyWard
{
    public Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        return Patient.Get(patientId, patientRepository, cancellationToken);
    }

    public Task<ImmutableList<Patient>> GetPatients(EmergencyPatientStatus[] status, CancellationToken cancellationToken)
    {
        return Patient.GetAll(status, patientRepository, cancellationToken);
    }

    public async Task<Encounter> RegisterPatient(string personalIdentifier, string familyName, string givenNames, string observation, CancellationToken cancellationToken)
    {
        var patient = await Patient.Register(personalIdentifier, familyName, givenNames, patientRepository, cancellationToken);
        var encounter = await Encounter.CreateNew(patient.Id, observation, encounterRepository, cancellationToken);

        return encounter;
    }

    public async Task<TransferredEvent> TransferPatient(Guid patientId, string wardIdentifier, string reason, CancellationToken cancellationToken)
    {
        var patient = await Patient.Transfer(patientId, wardIdentifier, patientRepository, cancellationToken);

        return new TransferredEvent(patient.PersonalIdentifier, patient.FamilyName, patient.GivenNames, wardIdentifier, reason);
    }
}
