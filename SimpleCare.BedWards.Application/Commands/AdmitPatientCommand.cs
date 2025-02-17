using MediatR;
using SimpleCare.BedWards.Application.Values;

namespace SimpleCare.BedWards.Application.Commands;

public record AdmitPatientCommand(PatientAdmission Admission) : IRequest;

public class AdmitPatientCommandHandler : IRequestHandler<AdmitPatientCommand>
{
    public Task Handle(AdmitPatientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
