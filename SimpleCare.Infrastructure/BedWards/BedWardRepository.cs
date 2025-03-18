using Microsoft.EntityFrameworkCore;

using SimpleCare.BedWards.Domain;
using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.BedWards;

public class BedWardRepository(SimpleCareDbContext dbContext) : IBedWardRepository
{
    private readonly DbSet<Ward> wards = dbContext.Set<Ward>();

    public async Task<Ward?> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken)
    {
        var ward = await wards.FirstOrDefaultAsync(w => w.Identifier == wardIdentifier, cancellationToken);
        return ward;
    }
}
