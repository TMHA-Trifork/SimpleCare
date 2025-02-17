using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.EmergencyWards.Values;

namespace SimpleCare.EmergencyWards.Domain;

public class EmergencyWardRoot(IEmergencyPatientRepository repository)
{
    public Patient RegisterPatient(string familyName, string givenNames)
    {
        var patient = new Patient(Guid.NewGuid(), familyName, givenNames);

        // TODO

        return patient;
    }
}
