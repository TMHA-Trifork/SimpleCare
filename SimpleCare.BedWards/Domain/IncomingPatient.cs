namespace SimpleCare.BedWards.Domain;

public enum IncomingStatus
{
    Pending,
    Admitted
}

public record IncomingPatient(Guid Id, Guid PatientId, Guid WardId, IncomingStatus Status);
