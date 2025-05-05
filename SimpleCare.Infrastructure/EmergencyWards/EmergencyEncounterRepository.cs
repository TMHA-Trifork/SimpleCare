using Microsoft.EntityFrameworkCore;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.EmergencyWards;

public class EmergencyEncounterRepository : IEmergencyEncounterRepository
{
    private DbSet<Encounter> encounters;

    public EmergencyEncounterRepository(SimpleCareDbContext dbContext)
    {
        encounters = dbContext.Set<Encounter>();
    }

    public async Task Add(Encounter encounter, CancellationToken cancellationToken)
    {
        await encounters.AddAsync(encounter, cancellationToken);
    }
}
