using Microsoft.EntityFrameworkCore;
using SimpleCare.EmergencyWards.Domain;
using SimpleCare.EmergencyWards.Interfaces;
using SimpleCare.Infrastructure.UnitOfWork;

namespace SimpleCare.Infrastructure.EmergencyWards;

public class EmergencyPatientRepository : IEmergencyPatientRepository
{
    private DbSet<Patient> patients;

    public EmergencyPatientRepository(SimpleCareDbContext dbContext)
    {
        patients = dbContext.Set<Patient>();
    }

    public async Task Add(Patient patient, CancellationToken cancellationToken)
    {
        await patients.AddAsync(patient, cancellationToken);
    }
}
