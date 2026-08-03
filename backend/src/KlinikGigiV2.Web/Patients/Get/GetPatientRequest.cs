namespace KlinikGigiV2.Web.Patients.Get;

public class GetPatientRequest
{
    public const string Route = "/klinik/patients/{PatientId}";

    public Guid PatientId { get; set; }
}


