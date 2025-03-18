namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWardIncomingPatientRepository
{
    Task<IncomingPatient?> GetIncomingPatientByPatientId(Guid patientId, CancellationToken cancellationToken);

    Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken);
    Task UpdateIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken);
}
