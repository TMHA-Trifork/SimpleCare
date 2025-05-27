using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public record Patient(Guid Id, string PersonalIdentifier, string FamilyName, string GivenNames, Guid WardId)
{
    internal static Task<Patient> Get(Guid patientId, IBedWardPatientRepository bedWardPatientRepository, CancellationToken cancellationToken)
    {
        return bedWardPatientRepository.Get(patientId, cancellationToken);
    }

    internal static async Task<Patient> Register(string personalIdentifier, string familyName, string givenNames, Guid wardId, IBedWardPatientRepository bedWardPatientRepository, CancellationToken cancellationToken)
    {
        var patient = await bedWardPatientRepository.GetByPersonalIdentifier(personalIdentifier, cancellationToken);
        if (patient is null)
        {
            patient = new Patient(Guid.NewGuid(), personalIdentifier, familyName, givenNames, wardId);
            await bedWardPatientRepository.Add(patient, cancellationToken);
        }

        return patient;
    }
}
