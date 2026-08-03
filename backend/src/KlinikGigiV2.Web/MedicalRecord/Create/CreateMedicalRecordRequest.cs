namespace KlinikGigiV2.Web.MedicalRecords.Create;

public class CreateMedicalRecordRequest
{
    public const string Route = "/klinik/patients/{patientId}/medical-records";

    public Guid PatientId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string Diagnosis { get; set; } = null!;
    public string Therapy { get; set; } = null!;
    public DateOnly VisitDate { get; set; }
    public string? Notes { get; set; }
}
