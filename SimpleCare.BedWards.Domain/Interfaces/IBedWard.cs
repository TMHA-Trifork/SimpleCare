namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWard
{
    Task<IEnumerable<Patient>> GetPatients(Guid wardId, CancellationToken cancellationToken);
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);

    Task<IEnumerable<Ward>> GetWards(CancellationToken cancellationToken);
    Task<Ward> GetWard(Guid wardId, CancellationToken cancellationToken);

    Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken);
    Task AdmitPatient(Guid patientId, Guid wardId, CancellationToken cancellationToken);
}
