using MediatR;
using SimpleCare.EmergencyWards.Application.Values;

namespace SimpleCare.EmergencyWards.Application.Commands;

public record TransferPatientCommand(TransferRequest TransferRequest) : IRequest;

public class TransferPatientCommandHandler : IRequestHandler<TransferPatientCommand>
{
    public Task Handle(TransferPatientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}