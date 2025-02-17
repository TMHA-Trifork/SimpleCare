using MediatR;
using SimpleCare.EmergencyWards.Application.Values;
using System.Security.Cryptography.X509Certificates;

namespace SimpleCare.EmergencyWards.Application.Commands;

public record RegisterPatientCommand(EmergencyRegistration EmergencyRegistration) : IRequest;

public class RegisterPatientCommandHandler() : IRequestHandler<RegisterPatientCommand>
{
    public Task Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}