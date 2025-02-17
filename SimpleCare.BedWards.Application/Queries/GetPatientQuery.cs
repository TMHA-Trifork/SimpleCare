using MediatR;
using SimpleCare.BedWards.Application.Values;

namespace SimpleCare.BedWards.Application.Queries;

public record GetPatientQuery(Guid patientId) : IRequest<BedWardPatient>;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, BedWardPatient>
{
    public Task<BedWardPatient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
