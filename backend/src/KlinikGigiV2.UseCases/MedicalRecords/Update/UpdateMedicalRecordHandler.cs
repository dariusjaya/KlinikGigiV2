using ErrorOr;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Update;

public class UpdateMedicalRecordHandler(IRepository<MedicalRecord> repository)
    : ICommandHandler<UpdateMedicalRecordCommand, ErrorOr<MedicalRecord>>
{
    public async ValueTask<ErrorOr<MedicalRecord>> Handle(
        UpdateMedicalRecordCommand request,
        CancellationToken ct)
    {
        var medicalRecord = await repository.GetByIdAsync(request.MedicalRecordId, ct);

        if (medicalRecord is null || medicalRecord.PatientId != request.PatientId)
        {
            return Error.NotFound(
                code: "MEDICAL_RECORD_NOT_FOUND",
                description: "Rekam medis tidak ditemukan.");
        }

        medicalRecord.UpdateVisitDate(request.VisitDate);
        medicalRecord.UpdateDiagnosis(request.Diagnosis);
        medicalRecord.UpdateTherapy(request.Therapy);
        medicalRecord.UpdateNotes(request.Notes);

        await repository.UpdateAsync(medicalRecord, ct);

        return medicalRecord;
    }
}