using NSubstitute;

using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Domain.Interfaces;

namespace SimpleCare.EmergencyWards.Tests;

public class PatientTest(PatientTestFixture fixture) : IClassFixture<PatientTestFixture>
{
    [Fact]
    public async Task Get_ShouldReturnPatient_IfPatientExists()
    {
        var patient = await Patient.Get(new Guid(PatientTestFixture.PatientId1), fixture.Repository, CancellationToken.None);

        Assert.Equal(new Guid(PatientTestFixture.PatientId1), patient.Id);
    }

    [Fact]
    public async Task Get_ShouldThrowException_IfPatientDoesNotExist()
    {
        var patientId = Guid.NewGuid();

        await Assert.ThrowsAnyAsync<Exception>(async () => await Patient.Get(Guid.NewGuid(), fixture.Repository, CancellationToken.None));
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllPatients_IfStatusIsEmpty()
    {
        var patients = await Patient.GetAll(Array.Empty<EmergencyPatientStatus>(), fixture.Repository, CancellationToken.None);
        Assert.Equal(3, patients.Count);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllPatients_IfStatusContainsAllValues()
    {
        var patients = await Patient.GetAll([EmergencyPatientStatus.Registered, EmergencyPatientStatus.InTransfer, EmergencyPatientStatus.Discharged], fixture.Repository, CancellationToken.None);
        Assert.Equal(3, patients.Count);
    }

    [Theory]
    [InlineData(EmergencyPatientStatus.Registered, PatientTestFixture.PatientId1)]
    [InlineData(EmergencyPatientStatus.InTransfer, PatientTestFixture.PatientId2)]
    [InlineData(EmergencyPatientStatus.Discharged, PatientTestFixture.PatientId3)]
    public async Task GetAll_ShouldReturnPatientWithCorrectStatus_WhenStatusIsGiven(EmergencyPatientStatus status, Guid patientId)
    {
        var patients = await Patient.GetAll([status], fixture.Repository, CancellationToken.None);
        Assert.Equal(patientId, patients.First().Id);
    }
}

public class PatientTestFixture
{
    public const string PatientId1 = "D21212DD-E59B-4077-89E0-ED75613C945B";
    public const string PatientId2 = "894EAEA6-9087-49D6-B155-385430D85ACB";
    public const string PatientId3 = "312D4966-3D14-44CA-93CA-AFC07942455A";

    public IEmergencyPatientRepository Repository { get; }

    public PatientTestFixture()
    {
        var patients = new List<Patient>
        {
            new(new Guid(PatientId1), "123456789", "Doe", "John", EmergencyPatientStatus.Registered),
            new(new Guid(PatientId2), "987654321", "Smith", "Jane", EmergencyPatientStatus.InTransfer),
            new(new Guid(PatientId3), "456789123", "Brown", "Alice", EmergencyPatientStatus.Discharged)
        };

        Repository = Substitute.For<IEmergencyPatientRepository>();

        Repository.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(callInfo =>
        {
            return patients.Exists(p => p.Id == callInfo.Arg<Guid>())
                ? patients.First(p => p.Id == callInfo.Arg<Guid>())
                : throw new Exception($"Patient with id={callInfo.Arg<Guid>()} not found");
        });

        Repository.GetAllWithStatusIn(Arg.Any<EmergencyPatientStatus[]>(), Arg.Any<CancellationToken>()).Returns(callInfo =>
        {
            return [.. patients.Where(p => callInfo.ArgAt<EmergencyPatientStatus[]>(0).Contains(p.Status))];
        });
    }
}
