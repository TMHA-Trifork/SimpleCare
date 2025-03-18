using MediatR;

using SimpleCare.BedWards.Application.Values;
using SimpleCare.BedWards.Domain.Interfaces;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;

namespace SimpleCare.BedWards.Application.Commands;

public record AdmitPatientCommand(PatientAdmission Admission) : IRequest;

public class AdmitPatientCommandHandler(IUnitOfWork unitOfWork, IBedWard bedWardRoot) : IRequestHandler<AdmitPatientCommand>
{
    public async Task Handle(AdmitPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await bedWardRoot.AdmitPatient(request.Admission.PatientId, cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while admitting the patient", ex);
        }
    }
}
