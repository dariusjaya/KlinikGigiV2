using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.UseCases.MedicalRecords.Get;

public record GetMedicalRecordQuery(Guid PatientId, Guid MedicalRecordId) : IQuery<Result<MedicalRecordRecord>>;
