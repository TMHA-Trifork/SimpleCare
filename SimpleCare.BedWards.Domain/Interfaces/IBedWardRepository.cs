

namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWardRepository
{
    Task<Ward> Get(Guid wardId, CancellationToken cancellationToken);
    Task<Ward?> GetByIdentifier(string wardIdentifier, CancellationToken cancellationToken);
    Task<IEnumerable<Ward>> GetAll(CancellationToken cancellationToken);
}
