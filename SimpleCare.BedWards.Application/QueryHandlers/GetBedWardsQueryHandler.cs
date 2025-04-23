using System.Collections.Immutable;

using MediatR;

using SimpleCare.BedWards.Application.Mappers;
using SimpleCare.BedWards.Boundary.Queries;
using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.BedWards.Domain.Interfaces;

namespace SimpleCare.BedWards.Application.QueryHandlers;

public class GetBedWardsQueryHandler(IBedWard bedWardRoot) : IRequestHandler<GetBedWardsQuery, ImmutableList<BedWardItem>>
{
    public async Task<ImmutableList<BedWardItem>> Handle(GetBedWardsQuery request, CancellationToken cancellationToken)
    {
        var wards = await bedWardRoot.GetWards(cancellationToken);
        return [.. wards.Select(w => w.Map())];
    }
}
