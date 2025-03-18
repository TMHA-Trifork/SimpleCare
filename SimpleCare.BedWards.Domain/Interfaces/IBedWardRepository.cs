using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Interfaces;

public interface IBedWardRepository
{
    Task<Ward?> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken);
}
