using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Core.Models;


namespace KlinikGigiV2.UseCases.MedicalRecords.List;

public record ListMedicalRecordQuery(Guid PatientId, string? Search, int? Page, int? PageSize) : IQuery<Result<PaginatedResponse<MedicalRecordRecord>>>;
