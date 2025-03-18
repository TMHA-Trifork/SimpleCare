namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWardPatientRepository
{
    Task<IEnumerable<Patient>> GetAll(CancellationToken cancellationToken);
    Task<Patient> Get(Guid patientId, CancellationToken cancellationToken);
    Task<Patient?> GetByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken);

    Task Add(Patient patient, CancellationToken cancellationToken);
}
