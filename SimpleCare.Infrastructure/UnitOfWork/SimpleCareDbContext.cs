using Microsoft.EntityFrameworkCore;
using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.Infrastructure.UnitOfWork;

public class SimpleCareDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Encounter> Encounters { get; set; }

    public SimpleCareDbContext(DbContextOptions<SimpleCareDbContext> options)
        : base(options) { }
}
