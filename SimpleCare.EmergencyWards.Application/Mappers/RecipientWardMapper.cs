using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.EmergencyWards.Application.Values;

namespace SimpleCare.EmergencyWards.Application.Mappers;

internal static class RecipientWardMapper
{
    internal static RecipientWardItem Map(this BedWardItem bedWardItem)
    {
        return new RecipientWardItem(
            bedWardItem.Id,
            bedWardItem.Identifier,
            bedWardItem.Name);
    }
}
