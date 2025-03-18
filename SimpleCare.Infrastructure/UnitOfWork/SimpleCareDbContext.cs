using Microsoft.EntityFrameworkCore;

namespace SimpleCare.Infrastructure.UnitOfWork;

public class SimpleCareDbContext : DbContext
{
    public DbSet<SimpleCare.EmergencyWards.Domain.Patient> EWPatients { get; set; }
    public DbSet<SimpleCare.EmergencyWards.Domain.Encounter> Encounters { get; set; }

    public DbSet<SimpleCare.BedWards.Domain.Patient> BWPatients { get; set; }
    public DbSet<SimpleCare.BedWards.Domain.IncomingPatient> BWIncomingPatients { get; set; }
    public DbSet<SimpleCare.BedWards.Domain.Ward> BWWards { get; set; }
    public DbSet<SimpleCare.BedWards.Domain.Encounter> BWEncounters { get; set; }

    public DbSet<SimpleCare.Orderlies.Domain.OrderlyTask> OLTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimpleCare.EmergencyWards.Domain.Patient>().ToTable("EW_Patients");
        modelBuilder.Entity<SimpleCare.EmergencyWards.Domain.Encounter>().ToTable("EW_Encounters");

        modelBuilder.Entity<SimpleCare.BedWards.Domain.Patient>().ToTable("BW_Patients");
        modelBuilder.Entity<SimpleCare.BedWards.Domain.IncomingPatient>().ToTable("BW_IncomingPatients");
        modelBuilder.Entity<SimpleCare.BedWards.Domain.Ward>().ToTable("BW_Ward")
            .HasData(
                new SimpleCare.BedWards.Domain.Ward(Guid.Parse("C5E5F332-8C68-4059-94EA-180CA17AB1E4"), "M1", "Medical Department 1"),
                new SimpleCare.BedWards.Domain.Ward(Guid.Parse("2EDD4F62-8B8A-437E-9EEF-5CB14DE87A94"), "M2", "Medical Department 2")
            );
        modelBuilder.Entity<SimpleCare.BedWards.Domain.Encounter>().ToTable("BW_Encounters");

        modelBuilder.Entity<SimpleCare.Orderlies.Domain.OrderlyTask>().ToTable("OL_Tasks");

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=localhost;Database=SimpleCare;User ID=sa;Password=WeLoveMicrosoft1234!;Encrypt=False");

        base.OnConfiguring(optionsBuilder);
    }
}
