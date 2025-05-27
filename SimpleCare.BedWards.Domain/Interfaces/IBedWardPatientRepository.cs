using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Interfaces;

public interface IBedWardPatientRepository
{
    Task<IEnumerable<Patient>> GetPatients(Guid wardId, CancellationToken cancellationToken);
    Task<Patient> Get(Guid patientId, CancellationToken cancellationToken);
    Task<Patient?> GetByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken);

    Task Add(Patient patient, CancellationToken cancellationToken);
}
