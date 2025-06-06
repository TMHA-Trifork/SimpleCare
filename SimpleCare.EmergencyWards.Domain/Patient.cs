﻿using SimpleCare.EmergencyWards.Interfaces;
using System.Collections.Immutable;

namespace SimpleCare.EmergencyWards.Domain;

public enum EmergencyPatientStatus
{
    Registered,
    InTransfer,
    Discharged
}

public record Patient(Guid Id, string PersonalIdentifier, string FamilyName, string GivenNames, EmergencyPatientStatus Status, string? WardIdentifier = null)
{
    internal static Task<Patient> Get(Guid patientId, IEmergencyPatientRepository patientRepository, CancellationToken cancellationToken)
    {
        return patientRepository.Get(patientId, cancellationToken);
    }

    internal static Task<ImmutableList<Patient>> GetAll(EmergencyPatientStatus[] status, IEmergencyPatientRepository patientRepository, CancellationToken cancellationToken)
    {
        if (status.Length == 0)
            status = [EmergencyPatientStatus.Registered, EmergencyPatientStatus.InTransfer, EmergencyPatientStatus.Discharged];

        return patientRepository.GetAllWithStatusIn(status, cancellationToken);
    }

    internal static async Task<Patient> Register(string personalIdentifier, string familyName, string givenNames, IEmergencyPatientRepository patientRepository, CancellationToken cancellationToken)
    {
        var patient = new Patient(Guid.NewGuid(), personalIdentifier, familyName, givenNames, EmergencyPatientStatus.Registered);
        await patientRepository.Add(patient, cancellationToken);

        return patient;
    }

    internal static async Task<Patient> Transfer(Guid patientId, string wardIdentifier, IEmergencyPatientRepository patientRepository, CancellationToken cancellationToken)
    {
        var patient = (await patientRepository.Get(patientId, cancellationToken))
            ?? throw new InvalidOperationException($"Patient with ID {patientId} not found.");

        if (patient.Status != EmergencyPatientStatus.Registered)
            throw new InvalidOperationException($"Patient with ID {patientId} is not in a state to be transferred.");

        patient = patient with
        {
            Status = EmergencyPatientStatus.InTransfer,
            WardIdentifier = wardIdentifier
        };

        await patientRepository.Update(patient, cancellationToken);

        return patient;
    }
}
