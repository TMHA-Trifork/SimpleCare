using SimpleCare.EmergencyWards.Boundary.Events;
using SimpleCare.EmergencyWards.Domain;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Interfaces;

public interface IEmergencyWard
{
    Task<ImmutableList<Patient>> GetPatients(CancellationToken cancellationToken);
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);
    Task<Encounter> RegisterPatient(string familyName, string givenNames, string observation, CancellationToken cancellationToken);
    Task<TransferredEvent> TransferPatient(Guid patientId, string familyName, string givenNames, string wardIdentifier, CancellationToken cancellationToken);
}
