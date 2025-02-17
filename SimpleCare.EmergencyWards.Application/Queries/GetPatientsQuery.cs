using MediatR;
using SimpleCare.EmergencyWards.Application.Values;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Application.Queries;

public record GetPatientsQuery : IRequest<ImmutableList<EmergencyPatientListItem>>;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, ImmutableList<EmergencyPatientListItem>>
{
    public Task<ImmutableList<EmergencyPatientListItem>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
