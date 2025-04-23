using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public record Ward(Guid Id, string Identifier, string Name)
{
    internal static async Task<Ward> Get(string wardIdentifier, IBedWardRepository bedWardRepository, CancellationToken cancellationToken)
    {
        return await bedWardRepository.GetByIdentifier(wardIdentifier, cancellationToken)
            ?? throw new InvalidOperationException($"Ward identifier={wardIdentifier} not found.");

    }

    internal static async Task<Ward> Get(Guid wardId, IBedWardRepository bedWardRepository, CancellationToken cancellationToken)
    {
        return await bedWardRepository.Get(wardId, cancellationToken);
    }

    internal static async Task<IEnumerable<Ward>> GetAll(IBedWardRepository bedWardRepository, CancellationToken cancellationToken)
    {
        return await bedWardRepository.GetAll(cancellationToken);
    }
}
