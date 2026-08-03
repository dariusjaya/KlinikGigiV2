using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.Core.MedicalRecordAggregate;

public record MedicalRecordRecord(
  Guid Id,
  Guid PatientId,
  string Diagnosis,
  DateOnly? VisitDate,
  string? Notes,
  string Therapy,
    Guid CreatedByUserId

)
{
    public static MedicalRecordRecord MapFromMedicalRecord(MedicalRecord medicalRecord) => new MedicalRecordRecord(
        Id: medicalRecord.Id,
        PatientId: medicalRecord.PatientId,
        Diagnosis: medicalRecord.Diagnosis,
        VisitDate: medicalRecord.VisitDate,
        Notes: medicalRecord.Notes,
        Therapy: medicalRecord.Therapy,
        CreatedByUserId: medicalRecord.CreatedByUserId
    );
}


