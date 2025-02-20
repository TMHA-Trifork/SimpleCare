using MediatR;

namespace SimpleCare.EmergencyWards.Boundary.Events;

public record TransferredEvent(Guid patientId, string familyName, string givenNames, string wardIdentifier) : INotification;
