using MediatR;

using SimpleCare.BedWards.Application.Mappers;
using SimpleCare.BedWards.Boundary.Queries;
using SimpleCare.BedWards.Boundary.Values;
using SimpleCare.BedWards.Domain.Interfaces;

using System.Diagnostics;

namespace SimpleCare.BedWards.Application.QueryHandlers;

public class GetBedWardQueryHandler(IBedWard bedWardRoot) : IRequestHandler<GetBedWardQuery, BedWardItem>
{
    private static readonly ActivitySource activitySource = new ActivitySource("SimpleCare");

    public async Task<BedWardItem> Handle(GetBedWardQuery request, CancellationToken cancellationToken)
    {
        using var activity = activitySource.StartActivity("GetBedWardQuery");

        activity?.SetTag("WardId", request.wardId);

        var bedWard = await bedWardRoot.GetWard(request.wardId, cancellationToken);

        return bedWard.Map();
    }
}
