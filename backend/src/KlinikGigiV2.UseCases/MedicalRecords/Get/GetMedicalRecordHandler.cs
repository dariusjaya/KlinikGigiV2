using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Get;

public class GetMedicalRecordHandler(IRepository<MedicalRecord> repository, IReadRepository<Patient> patientRepository) : IQueryHandler<GetMedicalRecordQuery, Result<MedicalRecordRecord>>
{


    public async ValueTask<Result<MedicalRecordRecord>> Handle(GetMedicalRecordQuery req, CancellationToken ct)
    {
        var patient = await patientRepository.GetByIdAsync(req.PatientId, ct);
        if (patient is null)
        {
            return Result.Error("Patient not found.");
        }
        var medicalRecord = await repository.GetByIdAsync(req.MedicalRecordId, ct);

        if (medicalRecord is null || medicalRecord.PatientId != req.PatientId)
        {
            return Result.Error("Medical record not found.");
        }

        var medicalRecordRecord = MedicalRecordRecord.MapFromMedicalRecord(medicalRecord);

        return Result.Success(medicalRecordRecord, "Medical record retrieved successfully.");
    }
}

