using MediatR;

using SimpleCare.EmergencyWards.Application.Mappers;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Domain.Interfaces;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;

namespace SimpleCare.EmergencyWards.Application.Commands;

public record TransferPatientCommand(TransferRequest TransferRequest) : IRequest;

public class TransferPatientCommandHandler(IUnitOfWork unitOfWork, IEmergencyWard emergencyWardRoot, IMediator mediator) : IRequestHandler<TransferPatientCommand>
{
    public async Task Handle(TransferPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transferredEvent = await emergencyWardRoot.TransferPatient(
                request.TransferRequest.PatientId,
                request.TransferRequest.WardIdentifier,
                request.TransferRequest.Reason,
                cancellationToken);

            await mediator.Publish(EmergencyEventsMapper.Map(transferredEvent), cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while transferring the patient", ex);
        }
    }
}