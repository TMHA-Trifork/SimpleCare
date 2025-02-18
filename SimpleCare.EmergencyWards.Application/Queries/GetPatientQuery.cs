using MediatR;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Interfaces;

namespace SimpleCare.EmergencyWards.Application.Queries;

public record GetPatientQuery(Guid PatientId) : IRequest<EmergencyPatient>;

public class GetPatientQueryHandler(IEmergencyWard emergencyWardRoot) : IRequestHandler<GetPatientQuery, EmergencyPatient>
{
    public async Task<EmergencyPatient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await emergencyWardRoot.GetPatient(request.PatientId, cancellationToken);
        return EmergencyPatient.Map(patient);
    }
}
