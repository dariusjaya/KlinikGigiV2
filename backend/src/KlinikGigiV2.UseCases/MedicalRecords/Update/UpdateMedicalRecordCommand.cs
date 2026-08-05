using ErrorOr;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Update;

public record UpdateMedicalRecordCommand(
    Guid PatientId,
    Guid MedicalRecordId,
    DateOnly VisitDate,
    string Diagnosis,
    string Therapy,
    string? Notes
) : ICommand<ErrorOr<MedicalRecord>>;