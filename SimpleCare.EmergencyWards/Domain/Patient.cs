namespace SimpleCare.EmergencyWards.Domain;

public enum EmergencyPatientStatus
{
    Registered,
    InTransfer,
    Discharged
}

public record Patient(Guid Id, string FamilyName, string GivenNames, EmergencyPatientStatus Status, string? wardIdentifier = null)
{
    internal Patient Transfer(string wardIdentifier)
    {
        return this with
        {
            Status = EmergencyPatientStatus.InTransfer,
            wardIdentifier = wardIdentifier
        };
    }
}
