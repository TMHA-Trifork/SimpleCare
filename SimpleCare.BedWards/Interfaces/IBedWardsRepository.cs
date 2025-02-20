using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Interfaces;

public interface IBedWardsRepository
{
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);
    Task<Patient> GetPatientByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken);
    Task<Ward> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken);

    Task AddPatient(Patient patient, CancellationToken cancellationToken);
    Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken);
}
