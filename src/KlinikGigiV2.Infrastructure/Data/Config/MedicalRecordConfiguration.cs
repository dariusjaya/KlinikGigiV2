using KlinikGigiV2.Core.MedicalRecordAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlinikGigiV2.Infrastructure.Data.Config;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecords");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Diagnosis)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Therapy)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.VisitDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.Patient)
            .WithMany(x => x.MedicalRecords)
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.MedicalRecords)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}