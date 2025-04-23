
using Microsoft.EntityFrameworkCore;

using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardRepository(SimpleCareDbContext dbContext) : IBedWardRepository
{
    private readonly DbSet<Ward> wards = dbContext.Set<Ward>();

    public async Task<Ward> Get(Guid wardId, CancellationToken cancellationToken)
    {
        var ward = await wards.FindAsync(wardId, cancellationToken)
            ?? throw new InvalidOperationException($"BedWard id={wardId} not found.");

        return ward;
    }

    public async Task<Ward?> GetByIdentifier(string wardIdentifier, CancellationToken cancellationToken)
    {
        var ward = await wards.FirstOrDefaultAsync(w => w.Identifier == wardIdentifier, cancellationToken);
        return ward;
    }
    public async Task<IEnumerable<Ward>> GetAll(CancellationToken cancellationToken)
    {
        return [.. await wards.ToListAsync(cancellationToken)];
    }
}
