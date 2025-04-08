namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWard
{
    Task<IEnumerable<Patient>> GetPatients(CancellationToken cancellationToken);
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);

    Task<IEnumerable<Ward>> GetWards(CancellationToken cancellationToken);

    Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken);
    Task AdmitPatient(Guid patientId, CancellationToken cancellationToken);
}
