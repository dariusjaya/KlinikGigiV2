namespace KlinikGigiV2.Web.MedicalRecords.List;

public class ListMedicalRecordRequest
{
    public const string Route = "/klinik/patients/{patientId}/medical-records";
    public Guid PatientId { get; set; }

    [QueryParam, BindFrom("search")]
    public string? Search { get; set; }

    [QueryParam, BindFrom("page")]
    public int? Page { get; set; }

    [QueryParam, BindFrom("pagesize")]
    public int? PageSize { get; set; }


}
