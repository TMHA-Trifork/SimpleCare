using SimpleCare.EmergencyWards.Domain;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Interfaces;

public interface IEmergencyPatientRepository
{
    Task<Patient> Get(Guid patientId, CancellationToken cancellationToken);
    Task<ImmutableList<Patient>> GetAll(CancellationToken cancellationToken);

    Task Add(Patient patient, CancellationToken cancellationToken);
    void Update(Patient patient, CancellationToken cancellationToken);
}
