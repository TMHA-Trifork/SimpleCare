using System.Collections.Immutable;

using MediatR;

using SimpleCare.BedWards.Boundary.Queries;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Application.Mappers;

namespace SimpleCare.EmergencyWards.Application.Queries;

public record GetRecipientWardsQuery : IRequest<ImmutableList<RecipientWardItem>>;

public class GetRecipientWardsQueryHandler(IMediator mediator) : IRequestHandler<GetRecipientWardsQuery, ImmutableList<RecipientWardItem>>
{
    public async Task<ImmutableList<RecipientWardItem>> Handle(GetRecipientWardsQuery request, CancellationToken cancellationToken)
    {
        var bedWardRequest = new GetBedWardsQuery();
        var result = await mediator.Send(bedWardRequest, cancellationToken);

        return [.. result.Select(w => w.Map())];
    }
}
