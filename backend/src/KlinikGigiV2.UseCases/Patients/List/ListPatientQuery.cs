using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core.PatientAggregate;


namespace KlinikGigiV2.UseCases.Patients.List;

public record ListPatientQuery(string? Search, int? Page, int? PageSize) : IQuery<Result<PaginatedResponse<PatientRecord>>>;
