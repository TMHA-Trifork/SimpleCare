using SimpleCare.BedWards.Interfaces;

namespace SimpleCare.BedWards.Domain;

public enum IncomingStatus
{
    Pending,
    Admitted
}

public record IncomingPatient(Guid Id, Guid PatientId, Guid WardId, IncomingStatus Status)
{
    internal static async Task Add(Guid patientId, Guid wardId, IBedWardIncomingPatientRepository bedWardIncomingPatientRepository, CancellationToken cancellationToken)
    {
        var incomingPatient = new IncomingPatient(Guid.NewGuid(), patientId, wardId, IncomingStatus.Pending);
        await bedWardIncomingPatientRepository.AddIncomingPatient(incomingPatient, cancellationToken);
    }

    internal static async Task SetAsAdmitted(Guid patientId, IBedWardIncomingPatientRepository bedWardIncomingPatientRepository, CancellationToken cancellationToken)
    {
        var incomingPatient = await bedWardIncomingPatientRepository.GetIncomingPatientByPatientId(patientId, cancellationToken);
        if (incomingPatient is not null && incomingPatient.Status != IncomingStatus.Admitted)
        {
            incomingPatient = incomingPatient with { Status = IncomingStatus.Admitted };
            await bedWardIncomingPatientRepository.UpdateIncomingPatient(incomingPatient, cancellationToken);
        }
    }
}
