using Ardalis.GuardClauses;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.UserAggregate;

namespace KlinikGigiV2.Core.MedicalRecordAggregate;

public class MedicalRecord : EntityBase<Guid>, IAuditableEntity
{
    public Guid PatientId { get; private set; }
    public DateOnly VisitDate { get; private set; }
    public string Diagnosis { get; private set; } = null!;
    public string Therapy { get; private set; } = null!;
    public string? Notes { get; private set; }

    public Guid CreatedByUserId { get; private set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Patient Patient { get; private set; } = null!;
    public User CreatedBy { get; private set; } = null!;

    public static MedicalRecord Create(
        Guid patientId,
        Guid createdByUserId,
        DateOnly visitDate,
        string diagnosis,
        string therapy,
        string? notes)
    {
        return new MedicalRecord
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            CreatedByUserId = createdByUserId,
            VisitDate = visitDate,
            Diagnosis = Guard.Against.NullOrWhiteSpace(diagnosis),
            Therapy = Guard.Against.NullOrWhiteSpace(therapy),
            Notes = notes
        };
    }

    public void UpdateDiagnosis(string diagnosis)
    {
        Diagnosis = Guard.Against.NullOrWhiteSpace(diagnosis);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTherapy(string therapy)
    {
        Therapy = Guard.Against.NullOrWhiteSpace(therapy);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateVisitDate(DateOnly visitDate)
    {
        VisitDate = Guard.Against.Default(visitDate);
        UpdatedAt = DateTime.UtcNow;
    }
}