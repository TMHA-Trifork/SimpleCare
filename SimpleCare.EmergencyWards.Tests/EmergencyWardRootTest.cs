using NSubstitute;
using SimpleCare.EmergencyWards.Domain.Interfaces;
using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.EmergencyWards.Tests;

public class EmergencyWardRootTest(EmergencyWardRootTestFixture fixture) : IClassFixture<EmergencyWardRootTestFixture>
{
    [Fact]
    public async Task Get_ShouldReturnPatient_IfPatientExists()
    {
        var patient = await fixture.EmergencyWard.GetPatient(new Guid(EmergencyWardRootTestFixture.PatientId1), CancellationToken.None);

        Assert.Equal(new Guid(EmergencyWardRootTestFixture.PatientId1), patient.Id);
    }

    [Fact]
    public async Task Get_ShouldThrowException_IfPatientDoesNotExist()
    {
        var patientId = Guid.NewGuid();

        await Assert.ThrowsAnyAsync<Exception>(async () => await fixture.EmergencyWard.GetPatient(Guid.NewGuid(), CancellationToken.None));
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllPatients_IfStatusIsEmpty()
    {
        var patients = await fixture.EmergencyWard.GetPatients(Array.Empty<EmergencyPatientStatus>(), CancellationToken.None);

        Assert.Equal(3, patients.Count);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllPatients_IfStatusContainsAllValues()
    {
        var patients = await fixture.EmergencyWard.GetPatients([EmergencyPatientStatus.Registered, EmergencyPatientStatus.InTransfer, EmergencyPatientStatus.Discharged], CancellationToken.None);
        Assert.Equal(3, patients.Count);
    }

    [Theory]
    [InlineData(EmergencyPatientStatus.Registered, EmergencyWardRootTestFixture.PatientId1)]
    [InlineData(EmergencyPatientStatus.InTransfer, EmergencyWardRootTestFixture.PatientId2)]
    [InlineData(EmergencyPatientStatus.Discharged, EmergencyWardRootTestFixture.PatientId3)]
    public async Task GetAll_ShouldReturnPatientWithCorrectStatus_WhenStatusIsGiven(EmergencyPatientStatus status, Guid patientId)
    {
        var patients = await fixture.EmergencyWard.GetPatients([status], CancellationToken.None);

        Assert.Equal(patientId, patients.First().Id);
    }

    [Fact]
    public async Task RegisterPatient_ShouldAddNewPatientAndEncounter()
    {
        var personalIdentifier = "123456789";
        var familyName = "Doe";
        var givenNames = "John";
        var observation = "Some observation";

        var encounter = await fixture.EmergencyWard.RegisterPatient(personalIdentifier, familyName, givenNames, observation, CancellationToken.None);

        Assert.NotNull(encounter);
        Assert.Equal(observation, encounter.EncounterReason);

        await fixture.PatientRepository.Received().Add(
            Arg.Is<Patient>(p =>
                p.PersonalIdentifier == personalIdentifier &&
                p.FamilyName == familyName &&
                p.GivenNames == givenNames &&
                p.Status == EmergencyPatientStatus.Registered),
            Arg.Any<CancellationToken>());

        await fixture.EncounterRepository.Received().Add(
            Arg.Is<Encounter>(e => e.EncounterReason == observation),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TransferPatient_ShouldSetPatientInTransferAndReturnEvent_IfPatientIsRegistered()
    {
        var patientId = new Guid(EmergencyWardRootTestFixture.PatientId1);
        var personalIdentifier = "123456789";
        var familyName = "Doe";
        var givenNames = "John";
        var wardIdentifier = "M1";
        var reason = "Transfer to another ward";

        var transferredEvent = await fixture.EmergencyWard.TransferPatient(patientId, wardIdentifier, reason, CancellationToken.None);

        Assert.Equal(personalIdentifier, transferredEvent.PersonalIdentifier);
        Assert.Equal(familyName, transferredEvent.FamilyName);
        Assert.Equal(givenNames, transferredEvent.GivenNames);
        Assert.Equal(wardIdentifier, transferredEvent.WardIdentifier);
        Assert.Equal(reason, transferredEvent.Reason);

        await fixture.PatientRepository.Received().Update(
            Arg.Is<Patient>(p =>
                p.Id == patientId &&
                p.PersonalIdentifier == personalIdentifier &&
                p.FamilyName == familyName &&
                p.GivenNames == givenNames &&
                p.Status == EmergencyPatientStatus.InTransfer &&
                p.WardIdentifier == wardIdentifier),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TransferPatient_ShouldFail_IfPatientIsInTransfer()
    {
        var patientId = new Guid(EmergencyWardRootTestFixture.PatientId2);
        var wardIdentifier = "M1";
        var reason = "Transfer to another ward";

        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await fixture.EmergencyWard.TransferPatient(patientId, wardIdentifier, reason, CancellationToken.None));
    }

    [Fact]
    public async Task TransferPatient_ShouldFail_IfPatientIsDischanged()
    {
        var patientId = new Guid(EmergencyWardRootTestFixture.PatientId3);
        var wardIdentifier = "M1";
        var reason = "Transfer to another ward";

        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await fixture.EmergencyWard.TransferPatient(patientId, wardIdentifier, reason, CancellationToken.None));
    }

    [Fact]
    public async Task TransferPatient_ShouldFail_IfPatientDoesNotExist()
    {
        var patientId = Guid.NewGuid();
        var wardIdentifier = "M1";
        var reason = "Transfer to another ward";

        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await fixture.EmergencyWard.TransferPatient(patientId, wardIdentifier, reason, CancellationToken.None));
    }
}

public class EmergencyWardRootTestFixture
{
    public const string PatientId1 = "D21212DD-E59B-4077-89E0-ED75613C945B";
    public const string PatientId2 = "894EAEA6-9087-49D6-B155-385430D85ACB";
    public const string PatientId3 = "312D4966-3D14-44CA-93CA-AFC07942455A";

    public IEmergencyPatientRepository PatientRepository { get; }
    public IEmergencyEncounterRepository EncounterRepository { get; }
    public IEmergencyWard EmergencyWard { get; }

    public EmergencyWardRootTestFixture()
    {
        var patients = new List<Patient>
        {
            new(new Guid(PatientId1), "123456789", "Doe", "John", EmergencyPatientStatus.Registered),
            new(new Guid(PatientId2), "987654321", "Smith", "Jane", EmergencyPatientStatus.InTransfer),
            new(new Guid(PatientId3), "456789123", "Brown", "Alice", EmergencyPatientStatus.Discharged)
        };

        PatientRepository = Substitute.For<IEmergencyPatientRepository>();

        PatientRepository.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(callInfo =>
        {
            return patients.Exists(p => p.Id == callInfo.Arg<Guid>())
                ? patients.First(p => p.Id == callInfo.Arg<Guid>())
                : throw new Exception($"Patient with id={callInfo.Arg<Guid>()} not found");
        });

        PatientRepository.GetAllWithStatusIn(Arg.Any<EmergencyPatientStatus[]>(), Arg.Any<CancellationToken>()).Returns(callInfo =>
        {
            return [.. patients.Where(p => callInfo.ArgAt<EmergencyPatientStatus[]>(0).Contains(p.Status))];
        });

        EncounterRepository = Substitute.For<IEmergencyEncounterRepository>();

        EmergencyWard = new EmergencyWardRoot(PatientRepository, EncounterRepository);
    }
}

