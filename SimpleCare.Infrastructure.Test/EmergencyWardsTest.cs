using Microsoft.Extensions.Options;

using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Domain.Interfaces;
using SimpleCare.Infrastructure.EmergencyWards;
using SimpleCare.Infrastructure.UnitOfWork;

using Testcontainers.MsSql;

namespace SimpleCare.Infrastructure.Test;

public class EmergencyWardsTest(EmergencyWardsTestFixure fixture) : IClassFixture<EmergencyWardsTestFixure>
{
    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientGet_ShouldReturnPatient_IfPatientExist()
    {
        var patient = await fixture.PatientRepository.Get(fixture.PatientId1, CancellationToken.None);

        Assert.Equal(fixture.PatientId1, patient.Id);
        Assert.Equal("123456789", patient.PersonalIdentifier);
        Assert.Equal("Doe", patient.FamilyName);
        Assert.Equal("John", patient.GivenNames);
        Assert.Equal(EmergencyPatientStatus.Registered, patient.Status);
    }

    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientGet_ShouldFail_IfPatientDoesNotExist()
    {
        var patientId = Guid.NewGuid();
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await fixture.PatientRepository.Get(patientId, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientGetAll_ShouldReturnNoPatients_IfNoStatusIsGiven()
    {
        var patients = await fixture.PatientRepository.GetAllWithStatusIn(Array.Empty<EmergencyPatientStatus>(), CancellationToken.None);

        Assert.Empty(patients);
    }

    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientGetAll_ShouldReturnAllPatients_IfAllStatusesIsGiven()
    {
        var patients = await fixture.PatientRepository.GetAllWithStatusIn(
            [
                EmergencyPatientStatus.Registered,
                EmergencyPatientStatus.InTransfer,
                EmergencyPatientStatus.Discharged
            ],
            CancellationToken.None);

        Assert.Contains(patients, p => p.Status == EmergencyPatientStatus.Registered);
        Assert.Contains(patients, p => p.Status == EmergencyPatientStatus.InTransfer);
        Assert.Contains(patients, p => p.Status == EmergencyPatientStatus.Discharged);
    }

    [Theory]
    [InlineData(EmergencyPatientStatus.Registered, 1)]
    [InlineData(EmergencyPatientStatus.InTransfer, 1)]
    [InlineData(EmergencyPatientStatus.Discharged, 1)]
    [Trait("Category", "Database")]
    public async Task PatientGetAll_ShouldReturnPatientsWithRightStatus_IfOneStatusIsGiven(EmergencyPatientStatus status, int minCount)
    {
        var patients = await fixture.PatientRepository.GetAllWithStatusIn([status], CancellationToken.None);

        Assert.True(patients.Where(p => p.Status == status).Count() >= minCount);
    }

    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientAdd_ShouldAddPatient()
    {
        var personalIdentifier = "111111111";
        var familyName = "Hobbs";
        var givenNames = "Jeremy";
        var status = EmergencyPatientStatus.Registered;

        var patient = new Patient(Guid.NewGuid(), personalIdentifier, familyName, givenNames, status);
        await fixture.PatientRepository.Add(patient, CancellationToken.None);

        var retrievedPatient = await fixture.PatientRepository.Get(patient.Id, CancellationToken.None);
        Assert.Equal(personalIdentifier, retrievedPatient.PersonalIdentifier);
        Assert.Equal(familyName, retrievedPatient.FamilyName);
        Assert.Equal(givenNames, retrievedPatient.GivenNames);
        Assert.Equal(status, retrievedPatient.Status);
    }

    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientUpdate_ShouldUpdatePatient_IfPatientExists()
    {
        var patient = await fixture.PatientRepository.Get(fixture.PatientId2, CancellationToken.None);

        var updatedPatient = patient with { FamilyName = "Smith" };
        await fixture.PatientRepository.Update(updatedPatient, CancellationToken.None);

        var retrievedPatient = await fixture.PatientRepository.Get(fixture.PatientId2, CancellationToken.None);
        Assert.Equal("Smith", retrievedPatient.FamilyName);
    }


    [Fact]
    [Trait("Category", "Database")]
    public async Task PatientUpdate_ShouldFail_IfPatientDoesNotExists()
    {
        var patient = await fixture.PatientRepository.Get(fixture.PatientId2, CancellationToken.None);

        var updatedPatient = patient with { Id = Guid.NewGuid(), FamilyName = "Smith" };
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await fixture.PatientRepository.Update(updatedPatient, CancellationToken.None));
    }
}

public class EmergencyWardsTestFixure : IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;

    private SimpleCareDbContext? dbContext;

    public IEmergencyPatientRepository PatientRepository => new EmergencyPatientRepository(dbContext);
    public IEmergencyEncounterRepository EncounterRepository => new EmergencyEncounterRepository(dbContext);

    public readonly Guid PatientId1 = Guid.NewGuid();
    public readonly Guid PatientId2 = Guid.NewGuid();
    public readonly Guid PatientId3 = Guid.NewGuid();

    public EmergencyWardsTestFixure()
    {
        msSqlContainer = new MsSqlBuilder().Build();
    }

    public async Task InitializeAsync()
    {
        await msSqlContainer.StartAsync();

        dbContext = new SimpleCareDbContext(
                Options.Create(
                    new SqlServerSettings
                    {
                        ConnectionString = msSqlContainer.GetConnectionString(),
                        Timeout = 30
                    }));
        await dbContext.Database.EnsureCreatedAsync();

        await dbContext.EWPatients.AddRangeAsync([
            new Patient(PatientId1, "123456789", "Doe", "John", EmergencyPatientStatus.Registered),
            new Patient(PatientId2, "987654321", "Smith", "Jane", EmergencyPatientStatus.InTransfer),
            new Patient(PatientId3, "456789123", "Brown", "Alice", EmergencyPatientStatus.Discharged)]);

        dbContext.SaveChanges();
    }

    public async Task DisposeAsync()
    {
        await msSqlContainer.DisposeAsync();
    }
}