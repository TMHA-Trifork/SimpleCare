using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Interfaces;

public interface IEmergencyWard
{
    Task<Encounter> RegisterPatient(string familyName, string givenNames, string observation, CancellationToken cancellationToken);
}
