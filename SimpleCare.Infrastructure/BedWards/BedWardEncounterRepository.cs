using Microsoft.EntityFrameworkCore;
using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardEncounterRepository(SimpleCareDbContext dbContext) : IBedWardEncounterRepository
{
    private readonly DbSet<Encounter> encounters = dbContext.Set<Encounter>();

    public async Task<Encounter?> GetActiveEncounterByPatientId(Guid patientId, CancellationToken cancellationToken)
    {
        var encounter = await encounters.FirstOrDefaultAsync(e => e.PatientId == patientId && e.Status == EncounterStatus.Admitted, cancellationToken);
        return encounter;
    }

    public async Task AddEncounter(Encounter encounter, CancellationToken cancellationToken)
    {
        await encounters.AddAsync(encounter, cancellationToken);
    }
}
