using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Application.Values;

public record EmergencyPatientListItem(Guid PatientId, string FamilyName, string GivenNames)
{
    internal static EmergencyPatientListItem Map(Patient patient)
    {
        return new EmergencyPatientListItem(patient.Id, patient.FamilyName, patient.GivenNames);
    }
}
