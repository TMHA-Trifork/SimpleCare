
using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public class BedWardRoot(IBedWardsRepository bedWardRepository) : IBedWard
{
    public Task<IEnumerable<Patient>> GetPatients(CancellationToken cancellationToken)
    {
        return bedWardRepository.GetAllPatients(cancellationToken);
    }

    public Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        return bedWardRepository.GetPatient(patientId, cancellationToken);
    }

    public async Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken)
    {
        var patient = await bedWardRepository.GetPatientByPersonalIdentifier(personalIdentifier, cancellationToken);
        if (patient is null)
        {
            patient = new Patient(Guid.NewGuid(), personalIdentifier, familyName, givenNames);
            await bedWardRepository.AddPatient(patient, cancellationToken);
        }

        var ward = await bedWardRepository.GetWardByIdentifier(wardIdentifier, cancellationToken);

        var incomingPatient = new IncomingPatient(Guid.NewGuid(), patient.Id, ward.Id, IncomingStatus.Pending);
        await bedWardRepository.AddIncomingPatient(incomingPatient, cancellationToken);
    }

    public async Task AdmitPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var encounter = await bedWardRepository.GetActiveEncounterByPatientId(patientId, cancellationToken);
        if (encounter is not null)
        {
            throw new InvalidOperationException("Patient already has an active encounter.");
        }

        var patient = await bedWardRepository.GetPatient(patientId, cancellationToken);

        var incomingPatient = await bedWardRepository.GetIncomingPatientByPatientId(patientId, cancellationToken);
        if (incomingPatient is not null && incomingPatient.Status != IncomingStatus.Admitted)
        {
            incomingPatient = incomingPatient with { Status = IncomingStatus.Admitted };
            await bedWardRepository.UpdateIncomingPatient(incomingPatient, cancellationToken);
        }

        encounter = new Encounter(Guid.NewGuid(), patient.Id, EncounterStatus.Admitted);
        await bedWardRepository.AddEncounter(encounter, cancellationToken);
    }
}
