using ErrorOr;
using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Create;

public class CreateMedicalRecordHandler(
    IRepository<MedicalRecord> medicalRecordRepository,
    IReadRepository<Patient> patientRepository)
    : ICommandHandler<CreateMedicalRecordCommand, ErrorOr<MedicalRecord>>
{
    public async ValueTask<ErrorOr<MedicalRecord>> Handle(
        CreateMedicalRecordCommand request,
        CancellationToken ct)
    {
        // Pastikan pasien memang ada
        var patient = await patientRepository.GetByIdAsync(request.PatientId, ct);
        if (patient is null)
        {
            return Error.NotFound(
                code: "PATIENT_NOT_FOUND",
                description: "Pasien tidak ditemukan.");
        }

        var medicalRecord = MedicalRecord.Create(
            patientId: request.PatientId,
            createdByUserId: request.CreatedByUserId,
            visitDate: request.VisitDate,
            diagnosis: request.Diagnosis,
            therapy: request.Therapy,
            notes: request.Notes
        );

        await medicalRecordRepository.AddAsync(medicalRecord, ct);

        return medicalRecord;
    }
}