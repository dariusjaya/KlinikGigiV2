using Ardalis.Specification;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.PatientAggregate.Specifications;
using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core;
using KlinikGigiV2.Core.MedicalRecordAggregate;
namespace KlinikGigiV2.UseCases.MedicalRecords.List;

public class ListMedicalRecordHandler(IRepository<MedicalRecord> repository) : IQueryHandler<ListMedicalRecordQuery, Result<PaginatedResponse<MedicalRecordRecord>>>
{
    private sealed class MedicalRecordListSpec : Specification<MedicalRecord>
    {
        public MedicalRecordListSpec(Guid patientId, string? search, int skip = 0, int take = 0)
        {

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerCaseSearch = search.ToLower();

                Query
                  .Where(medicalRecord => medicalRecord.PatientId == patientId)
                  .Search(medicalRecord => medicalRecord.Diagnosis.ToLower(), $"%{lowerCaseSearch}%");
            }

            if (skip >= 0 && take > 0)
            {
                Query.OrderBy(medicalRecord => medicalRecord.CreatedAt).Skip(skip).Take(take);
            }
        }
    }

    public async ValueTask<Result<PaginatedResponse<MedicalRecordRecord>>> Handle(ListMedicalRecordQuery req, CancellationToken ct)
    {
        PaginationProps paginatedData = Helper.PaginationHelper(100, req.Page, req.PageSize);

        var medicalRecordSpec = new MedicalRecordListSpec(req.PatientId, req.Search, paginatedData.Skip, paginatedData.Take);
        var result = await repository.ListAsync(medicalRecordSpec, ct);

        var countSpec = new MedicalRecordListSpec(req.PatientId, req.Search);
        var count = await repository.CountAsync(countSpec, ct);

        var medicalRecordRecords = result.Select(MedicalRecordRecord.MapFromMedicalRecord).ToList();

        PaginatedResponse<MedicalRecordRecord> paginatedResponse = new()
        {
            Items = medicalRecordRecords,
            TotalItems = count,
            Page = paginatedData.Page,
            PageSize = paginatedData.PageSize
        };

        return Result.Success(paginatedResponse, "Medical record list fetched successfully.");
    }
}

