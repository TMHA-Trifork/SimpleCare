using SimpleCare.BedWards.Domain.Interfaces;

namespace SimpleCare.BedWards.Domain;

public class BedWardRoot(
    IBedWardPatientRepository bedWardPatientRepository,
    IBedWardIncomingPatientRepository bedWardIncomingPatientRepository,
    IBedWardEncounterRepository bedWardEncounterRepository,
    IBedWardRepository bedWardRepository) : IBedWard
{
    public Task<IEnumerable<Patient>> GetPatients(CancellationToken cancellationToken)
    {
        return bedWardPatientRepository.GetAll(cancellationToken);
    }

    public async Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        return await Patient.Get(patientId, bedWardPatientRepository, cancellationToken);
    }

    public async Task<IEnumerable<Ward>> GetWards(CancellationToken cancellationToken)
    {
        return await Ward.GetAll(bedWardRepository, cancellationToken);
    }

    public async Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken)
    {
        var ward = await Ward.Get(wardIdentifier, bedWardRepository, cancellationToken);
        var patient = await Patient.Register(personalIdentifier, familyName, givenNames, bedWardPatientRepository, cancellationToken);

        await IncomingPatient.Add(patient.Id, ward.Id, bedWardIncomingPatientRepository, cancellationToken);
    }

    public async Task AdmitPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var found = await Encounter.HasActiveForPatientId(patientId, bedWardEncounterRepository, cancellationToken);
        if (found)
            throw new InvalidOperationException("Patient already has an active encounter.");

        var patient = await Patient.Get(patientId, bedWardPatientRepository, cancellationToken);

        await IncomingPatient.SetAsAdmitted(patient.Id, bedWardIncomingPatientRepository, cancellationToken);
        await Encounter.AddNewAdmittance(patient.Id, bedWardEncounterRepository, cancellationToken);
    }
}
