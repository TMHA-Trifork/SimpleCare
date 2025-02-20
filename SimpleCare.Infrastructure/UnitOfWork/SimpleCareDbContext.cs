using Microsoft.EntityFrameworkCore;
using SimpleCare.EmergencyWards.Domain;

namespace SimpleCare.Infrastructure.UnitOfWork;

public class SimpleCareDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Encounter> Encounters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=SimpleCare;User ID=sa;Password=WeLoveMicrosoft1234!;Encrypt=False");

        base.OnConfiguring(optionsBuilder);
    }
}
