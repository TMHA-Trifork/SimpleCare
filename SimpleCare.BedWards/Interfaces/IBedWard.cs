namespace SimpleCare.BedWards.Interfaces;

public interface IBedWard
{
    Task RegisterIncomingPatient(string personalIdentifier, string familyName, string givenNames, string wardIdentifier, string reason, CancellationToken cancellationToken);
}
