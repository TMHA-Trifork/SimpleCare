namespace SimpleCare.BedWards.Domain.Interfaces;

public interface IBedWardRepository
{
    Task<Ward?> GetWardByIdentifier(string wardIdentifier, CancellationToken cancellationToken);
}
