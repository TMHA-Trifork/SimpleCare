using Microsoft.EntityFrameworkCore;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardsRepository : IBedWardsRepository
{
    private DbSet<Patient> patients;
    private DbSet<IncomingPatient> incomingPatients;
    private DbSet<Ward> wards;

    public BedWardsRepository(SimpleCareDbContext dbContext)
    {
        patients = dbContext.Set<Patient>();
        incomingPatients = dbContext.Set<IncomingPatient>();
        wards = dbContext.Set<Ward>();
    }

    public async Task<Patient> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var patient = (await patients.FindAsync(patientId, cancellationToken))
            ?? throw new Exception($"Patient with id={patientId} not found");

        return patient;
    }

    public async Task<Patient> GetPatientByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken)
    {
        var patient = await patients.FirstOrDefaultAsync(p => p.PersonalIdentifier == personalIdentifier, cancellationToken)
            ?? throw new Exception($"Patient with personalIdentifier={personalIdentifier} not found");

        return patient;
    }

    public async Task<Ward> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken)
    {
        var ward = (await wards.FindAsync(wardIdentifier, cancellationToken))
            ?? throw new Exception($"Ward with wardIdentifier={wardIdentifier} not found");

        return ward;
    }

    public async Task AddPatient(Patient patient, CancellationToken cancellationToken)
    {
        await patients.AddAsync(patient, cancellationToken);
    }

    public async Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken)
    {
        await incomingPatients.AddAsync(incomingPatient, cancellationToken);
    }
}
