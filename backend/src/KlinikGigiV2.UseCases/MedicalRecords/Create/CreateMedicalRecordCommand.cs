using ErrorOr;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Create;

public record CreateMedicalRecordCommand
(
    Guid PatientId,
    Guid CreatedByUserId,
    string Diagnosis,
    string Therapy,
    DateOnly VisitDate,
    string? Notes
) : ICommand<ErrorOr<MedicalRecord>>;

