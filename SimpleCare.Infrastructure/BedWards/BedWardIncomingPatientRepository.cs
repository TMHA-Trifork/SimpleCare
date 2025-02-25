using Microsoft.EntityFrameworkCore;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardIncomingPatientRepository(SimpleCareDbContext dbContext) : IBedWardIncomingPatientRepository
{
    private readonly DbSet<IncomingPatient> incomingPatients = dbContext.Set<IncomingPatient>();

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

    public async Task AddIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken)
    {
        await incomingPatients.AddAsync(incomingPatient, cancellationToken);
    }

    public async Task UpdateIncomingPatient(IncomingPatient incomingPatient, CancellationToken cancellationToken)
    {
        var p = await GetIncomingPatient(incomingPatient.Id, cancellationToken);
        incomingPatients.Entry(p).CurrentValues.SetValues(incomingPatient);
    }
}
