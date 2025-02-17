using MediatR;
using SimpleCare.BedWards.Application.Values;
using System.Collections.Immutable;

namespace SimpleCare.BedWards.Application.Queries;

public record GetPatientsQuery : IRequest<ImmutableList<BedWardPatientListItem>>;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, ImmutableList<BedWardPatientListItem>>
{
    public Task<ImmutableList<BedWardPatientListItem>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
