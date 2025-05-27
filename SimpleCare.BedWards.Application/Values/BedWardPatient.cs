using SimpleCare.BedWards.Domain;

namespace SimpleCare.BedWards.Application.Values;

public record BedWardPatient(Guid Id, string PersonalIdentifier, string FamilyName, string GivenNames, Guid WardId)
{
    internal static BedWardPatient Map(Patient patient)
    {
        return new BedWardPatient(patient.Id, patient.PersonalIdentifier, patient.FamilyName, patient.GivenNames, patient.WardId);
    }
}
