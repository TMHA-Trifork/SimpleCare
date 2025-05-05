using MediatR;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Application.Queries;

public record GetPatientsQuery(EmergencyPatientStatus[] Status) : IRequest<ImmutableList<EmergencyPatientListItem>>;

public class GetPatientsQueryHandler(IEmergencyWard emergencyWardRoot) : IRequestHandler<GetPatientsQuery, ImmutableList<EmergencyPatientListItem>>
{
    public async Task<ImmutableList<EmergencyPatientListItem>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await emergencyWardRoot.GetPatients(request.Status, cancellationToken);
        return [.. patients.Select(p => EmergencyPatientListItem.Map(p))];
    }
}
