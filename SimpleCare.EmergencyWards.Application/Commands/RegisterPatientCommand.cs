using MediatR;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.Interfaces.UnitOfWork;

namespace SimpleCare.EmergencyWards.Application.Commands;

public record RegisterPatientCommand(EmergencyRegistration EmergencyRegistration) : IRequest;

public class RegisterPatientCommandHandler(IUnitOfWork unitOfWork, IEmergencyWard emergencyWardRoot) : IRequestHandler<RegisterPatientCommand>
{
    public async Task Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await emergencyWardRoot.RegisterPatient(
                request.EmergencyRegistration.PersonalIdentifier,
                request.EmergencyRegistration.FamilyName,
                request.EmergencyRegistration.GivenNames,
                request.EmergencyRegistration.Reason,
                cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while registering the patient", ex);
        }
    }
}