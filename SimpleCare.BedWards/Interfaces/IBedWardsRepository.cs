
using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Interfaces;

public interface IBedWardsRepository
{
    Task<IEnumerable<Patient>> GetAllPatients(CancellationToken cancellationToken);
    Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken);
    Task<Patient?> GetPatientByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken);

    Task<Ward?> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken);

    Task<IncomingPatient?> GetIncomingPatientByPatientId(Guid patientId, CancellationToken cancellationToken);

    Task<Encounter> GetActiveEncounterByPatientId(Guid patientId, CancellationToken cancellationToken);

    Task AddPatient(Patient patient, CancellationToken cancellationToken);

    Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken);
    Task UpdateIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken);

    Task AddEncounter(Encounter encounter, CancellationToken cancellationToken);
}
