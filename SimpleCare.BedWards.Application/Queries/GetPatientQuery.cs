using MediatR;
using SimpleCare.BedWards.Application.Values;
using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Application.Queries;

public record GetPatientQuery(Guid PatientId) : IRequest<BedWardPatient>;

public class GetPatientQueryHandler(IBedWard bedWardRoot) : IRequestHandler<GetPatientQuery, BedWardPatient>
{
    public async Task<BedWardPatient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await bedWardRoot.GetPatient(request.PatientId, cancellationToken);
        return BedWardPatient.Map(patient);
    }
}
