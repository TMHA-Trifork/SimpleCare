using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Application.Mappers;

internal static class WardMapper
{
    internal static BedWardItem Map(this Ward ward)
    {
        return new BedWardItem(
            ward.Id,
            ward.Identifier,
            ward.Name);
    }
}
