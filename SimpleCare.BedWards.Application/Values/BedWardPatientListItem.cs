using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Application.Values;

public record BedWardPatientListItem(Guid Id, string PersonalIdentifier, string FamilyName, string GivenNames)
{
    internal static BedWardPatientListItem Map(Patient patient)
    {
        return new BedWardPatientListItem(patient.Id, patient.PersonalIdentifier, patient.FamilyName, patient.GivenNames);
    }
}
