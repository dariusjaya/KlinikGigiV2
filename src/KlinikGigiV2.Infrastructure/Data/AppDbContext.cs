using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.UserAggregate;

namespace KlinikGigiV2.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users => Set<User>();
  public DbSet<Patient> Patients => Set<Patient>();
  public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();



  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
