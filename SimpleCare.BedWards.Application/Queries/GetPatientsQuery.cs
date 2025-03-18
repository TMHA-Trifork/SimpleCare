using MediatR;

using SimpleCare.BedWards.Application.Values;
using SimpleCare.BedWards.Domain.Interfaces;

using System.Collections.Immutable;

namespace SimpleCare.BedWards.Application.Queries;

public record GetPatientsQuery : IRequest<ImmutableList<BedWardPatientListItem>>;

public class GetPatientsQueryHandler(IBedWard bedWardRoot) : IRequestHandler<GetPatientsQuery, ImmutableList<BedWardPatientListItem>>
{
    public async Task<ImmutableList<BedWardPatientListItem>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await bedWardRoot.GetPatients(cancellationToken);
        return [.. patients.Select(p => BedWardPatientListItem.Map(p))];
    }
}
