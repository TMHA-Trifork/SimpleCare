using SimpleCare.EmergencyWards.Domain.Events;

using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Domain.Interfaces;

public interface IEmergencyWard
{
    Task<ImmutableList<Patient>> GetPatients(EmergencyPatientStatus[] status, CancellationToken cancellationToken);
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);

    Task<Encounter> RegisterPatient(string personalIdentifier, string familyName, string givenNames, string observation, CancellationToken cancellationToken);
    Task<TransferredEvent> TransferPatient(Guid patientId, string wardIdentifier, string reason, CancellationToken cancellationToken);
}
