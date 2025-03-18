using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public record Ward(Guid Id, string Identifier, string Name)
{
    internal static async Task<Ward> Get(string wardIdentifier, IBedWardRepository bedWardRepository, CancellationToken cancellationToken)
    {
        return await bedWardRepository.GetWardByIdentifier(wardIdentifier, cancellationToken)
            ?? throw new InvalidOperationException($"Ward identifier={wardIdentifier} not found.");

    }
}
