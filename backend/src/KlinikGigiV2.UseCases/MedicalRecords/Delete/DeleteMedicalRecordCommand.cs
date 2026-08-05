using ErrorOr;

namespace KlinikGigiV2.UseCases.MedicalRecords.Delete;

public record DeleteMedicalRecordCommand(
    Guid PatientId,
    Guid MedicalRecordId
) : ICommand<ErrorOr<Success>>;