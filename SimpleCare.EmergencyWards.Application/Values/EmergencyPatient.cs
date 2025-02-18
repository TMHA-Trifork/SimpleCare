using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Application.Values;

public record EmergencyPatient(Guid PatientId, string FamilyName, string GivenNames)
{
    internal static EmergencyPatient Map(Patient p)
    {
        return new EmergencyPatient(p.Id, p.FamilyName, p.GivenNames);
    }
}
