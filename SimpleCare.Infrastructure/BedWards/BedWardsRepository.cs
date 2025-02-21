using Microsoft.EntityFrameworkCore;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardsRepository(SimpleCareDbContext dbContext) : IBedWardsRepository
{
    private readonly DbSet<Patient> patients = dbContext.Set<Patient>();
    private readonly DbSet<IncomingPatient> incomingPatients = dbContext.Set<IncomingPatient>();
    private readonly DbSet<Ward> wards = dbContext.Set<Ward>();
    private readonly DbSet<Encounter> encounters = dbContext.Set<Encounter>();

    public async Task<IEnumerable<Patient>> GetAllPatients(CancellationToken cancellationToken)
    {
        return [.. await patients.ToListAsync(cancellationToken)];
    }

    public async Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var patient = (await patients.FindAsync(patientId, cancellationToken))
            ?? throw new Exception($"Patient with id={patientId} not found");

        return patient;
    }

    public async Task<Patient?> GetPatientByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken)
    {
        var patient = await patients.FirstOrDefaultAsync(p => p.PersonalIdentifier == personalIdentifier, cancellationToken);
        return patient;
    }

    public async Task<Ward?> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken)
    {
        var ward = await wards.FirstOrDefaultAsync(w => w.Identifier == wardIdentifier, cancellationToken);
        return ward;
    }

    public async Task<IncomingPatient> GetIncomingPatient(Guid incomingPatientId, CancellationToken cancellationToken)
    {
        var patient = (await incomingPatients.FindAsync(incomingPatientId, cancellationToken))
            ?? throw new Exception($"IncomingPatient with id={incomingPatientId} not found");

        return patient;
    }

    public async Task<IncomingPatient?> GetIncomingPatientByPatientId(Guid patientId, CancellationToken cancellationToken)
    {
        var incomingPatient = await incomingPatients.FirstOrDefaultAsync(p => p.PatientId == patientId, cancellationToken);
        return incomingPatient;
    }

    public async Task<Encounter> GetActiveEncounterByPatientId(Guid patientId, CancellationToken cancellationToken)
    {
        var encounter = await encounters.FirstOrDefaultAsync(e => e.PatientId == patientId && e.Status == EncounterStatus.Admitted, cancellationToken);
        return encounter;
    }

    public async Task AddPatient(Patient patient, CancellationToken cancellationToken)
    {
        await patients.AddAsync(patient, cancellationToken);
    }

    public async Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken)
    {
        await incomingPatients.AddAsync(incomingPatient, cancellationToken);
    }

    public async Task UpdateIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken)
    {
        var p = await GetIncomingPatient(incomingPatient.Id, cancellationToken);
        incomingPatients.Entry(p).CurrentValues.SetValues(incomingPatient);
    }

    public async Task AddEncounter(Encounter encounter, CancellationToken cancellationToken)
    {
        await encounters.AddAsync(encounter, cancellationToken);
    }
}
