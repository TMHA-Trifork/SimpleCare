using Microsoft.EntityFrameworkCore;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardPatientRepository(SimpleCareDbContext dbContext) : IBedWardPatientRepository
{
    private readonly DbSet<Patient> patients = dbContext.Set<Patient>();

    public async Task<IEnumerable<Patient>> GetAll(CancellationToken cancellationToken)
    {
        return [.. await patients.ToListAsync(cancellationToken)];
    }

    public async Task<Patient> Get(Guid patientId, CancellationToken cancellationToken)
    {
        var patient = (await patients.FindAsync(patientId, cancellationToken))
            ?? throw new Exception($"Patient with id={patientId} not found");

        return patient;
    }

    public async Task<Patient?> GetByPersonalIdentifier(string personalIdentifier, CancellationToken cancellationToken)
    {
        var patient = await patients.FirstOrDefaultAsync(p => p.PersonalIdentifier == personalIdentifier, cancellationToken);
        return patient;
    }

    public async Task Add(Patient patient, CancellationToken cancellationToken)
    {
        await patients.AddAsync(patient, cancellationToken);
    }
}
