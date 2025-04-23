using MediatR;

using SimpleCare.BedWards.Application.Mappers;
using SimpleCare.BedWards.Boundary.Queries;
using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.BedWards.Domain.Interfaces;

namespace SimpleCare.BedWards.Application.QueryHandlers;

public class GetBedWardQueryHandler(IBedWard bedWardRoot) : IRequestHandler<GetBedWardQuery, BedWardItem>
{
    public async Task<BedWardItem> Handle(GetBedWardQuery request, CancellationToken cancellationToken)
    {
        var bedWard = await bedWardRoot.GetWard(request.wardId, cancellationToken);

        return bedWard.Map();
    }
}
