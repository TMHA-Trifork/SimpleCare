using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Interfaces;

public interface IEmergencyPatientRepository
{
    Task Add(Patient patient, CancellationToken cancellationToken);
}
