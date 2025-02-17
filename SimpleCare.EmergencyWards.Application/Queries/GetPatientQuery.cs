using MediatR;
using SimpleCare.EmergencyWards.Application.Values;

namespace SimpleCare.EmergencyWards.Application.Queries;

public record GetPatientQuery(Guid PatientId) : IRequest<EmergencyPatient>;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, EmergencyPatient>
{
    public Task<EmergencyPatient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
