using Microsoft.EntityFrameworkCore;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;
using System.Collections.Immutable;

namespace SimpleCare.Infrastructure.EmergencyWards;

public class EmergencyPatientRepository : IEmergencyPatientRepository
{
    private readonly DbSet<Patient> patients;

    public EmergencyPatientRepository(SimpleCareDbContext dbContext)
    {
        patients = dbContext.Set<Patient>();
    }

    public async Task<Patient> Get(Guid patientId, CancellationToken cancellationToken)
    {
        var patient = (await patients.FindAsync(patientId, cancellationToken))
            ?? throw new Exception($"Patient with id={patientId} not found");
        return patient;
    }

    public async Task<ImmutableList<Patient>> GetAllWithStatusIn(EmergencyPatientStatus[] status, CancellationToken cancellationToken)
    {
        return [.. await patients.Where(p => status.Contains(p.Status)).ToListAsync(cancellationToken)];
    }

    public async Task Add(Patient patient, CancellationToken cancellationToken)
    {
        await patients.AddAsync(patient, cancellationToken);
    }

    public async void Update(Patient patient, CancellationToken cancellationToken)
    {
        var p = await Get(patient.Id, cancellationToken);
        patients.Entry(p).CurrentValues.SetValues(patient);
    }
}
