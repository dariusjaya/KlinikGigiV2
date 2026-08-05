using ErrorOr;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Delete;

public class DeleteMedicalRecordHandler(IRepository<MedicalRecord> repository)
    : ICommandHandler<DeleteMedicalRecordCommand, ErrorOr<Success>>
{
    public async ValueTask<ErrorOr<Success>> Handle(
        DeleteMedicalRecordCommand request,
        CancellationToken ct)
    {
        var medicalRecord = await repository.GetByIdAsync(request.MedicalRecordId, ct);

        if (medicalRecord is null || medicalRecord.PatientId != request.PatientId)
        {
            return Error.NotFound(
                code: "MEDICAL_RECORD_NOT_FOUND",
                description: "Rekam medis tidak ditemukan.");
        }

        await repository.DeleteAsync(medicalRecord, ct);

        return ErrorOr.Result.Success;
    }
}