using Ardalis.Specification;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.PatientAggregate.Specifications;
using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core;
namespace KlinikGigiV2.UseCases.Patients.List;

public class ListPatientHandler(IRepository<Patient> repository) : IQueryHandler<ListPatientQuery, Result<PaginatedResponse<PatientRecord>>>
{
    private sealed class PatientListSpec : Specification<Patient>
    {
        public PatientListSpec(string? search, int skip = 0, int take = 0)
        {

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerCaseSearch = search.ToLower();

                Query
                  .Search(patient => patient.FullName.ToLower(), $"%{lowerCaseSearch}%");
            }

            if (skip >= 0 && take > 0)
            {
                Query.OrderBy(patient => patient.CreatedAt).Skip(skip).Take(take);
            }
        }
    }

    public async ValueTask<Result<PaginatedResponse<PatientRecord>>> Handle(ListPatientQuery req, CancellationToken ct)
    {
        PaginationProps paginatedData = Helper.PaginationHelper(100, req.Page, req.PageSize);

        var patientSpec = new PatientListSpec(req.Search, paginatedData.Skip, paginatedData.Take);
        var result = await repository.ListAsync(patientSpec, ct);

        var countSpec = new PatientListSpec(req.Search);
        var count = await repository.CountAsync(countSpec, ct);

        var patientRecords = result.Select(PatientRecord.MapFromPatient).ToList();

        PaginatedResponse<PatientRecord> paginatedResponse = new()
        {
            Items = patientRecords,
            TotalItems = count,
            Page = paginatedData.Page,
            PageSize = paginatedData.PageSize
        };

        return Result.Success(paginatedResponse, "Patient list fetched successfully.");
    }
}

