using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public class BedWardRoot(IBedWardsRepository bedWardRepository) : IBedWard
{
    public async Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken)
    {
        var patient = await bedWardRepository.GetPatientByPersonalIdentifier(personalIdentifier, cancellationToken);
        if (patient is null)
        {
            patient = new Patient(Guid.NewGuid(), personalIdentifier, familyName, givenNames);
            await bedWardRepository.AddPatient(patient, cancellationToken);
        }

        var ward = await bedWardRepository.GetWardByIdentifier(wardIdentifier, cancellationToken);

        var incomingPatient = new IncomingPatient(Guid.NewGuid(), patient.Id, ward.Id, IncomingStatus.Pending);
        await bedWardRepository.AddIncomingPatient(incomingPatient, cancellationToken);
    }
}
