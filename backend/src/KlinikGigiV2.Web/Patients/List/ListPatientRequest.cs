namespace KlinikGigiV2.Web.Patients.List;

public class ListPatientRequest
{
    public const string Route = "/klinik/patients";

    [QueryParam, BindFrom("search")]
    public string? Search { get; set; }

    [QueryParam, BindFrom("page")]
    public int? Page { get; set; }

    [QueryParam, BindFrom("pagesize")]
    public int? PageSize { get; set; }


}
