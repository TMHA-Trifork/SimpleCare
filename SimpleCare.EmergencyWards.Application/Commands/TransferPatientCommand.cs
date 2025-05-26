using System.Diagnostics;
using System.Diagnostics.Metrics;

using MediatR;

using Microsoft.Extensions.Logging;

using SimpleCare.BedWards.Boundary.Queries;
using SimpleCare.EmergencyWards.Application.Mappers;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;

namespace SimpleCare.EmergencyWards.Application.Commands;

public record TransferPatientCommand(TransferRequest TransferRequest) : IRequest;

public class TransferPatientCommandHandler(IUnitOfWork unitOfWork, IEmergencyWard emergencyWardRoot, IMediator mediator) : IRequestHandler<TransferPatientCommand>
{
    private static readonly Meter Meter = new Meter("SimpleCare");
    private static readonly Counter<long> TransferPatientCounter = Meter.CreateCounter<long>("TransferPatientCounter", "count", "Counts the number of patients transferred");

    private static readonly ActivitySource activitySource = new ActivitySource("SimpleCare");

    public async Task Handle(TransferPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var activity = activitySource.StartActivity("TransferPatientCommand");

            activity?.SetTag("PatientId", request.TransferRequest.PatientId);

            var wardQuery = new GetBedWardQuery(request.TransferRequest.WardId);
            var bedWard = await mediator.Send(wardQuery);

            var transferredEvent = await emergencyWardRoot.TransferPatient(
                request.TransferRequest.PatientId,
                bedWard.Identifier,
                request.TransferRequest.Reason,
                cancellationToken);

            await mediator.Publish(transferredEvent.MapWith(bedWard), cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);

            TransferPatientCounter.Add(1);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"An error occurred while transferring patient with id='{request.TransferRequest.PatientId}'", ex);
        }
    }
}