namespace KlinikGigiV2.Web.MedicalRecords.Update;

public class UpdateMedicalRecordRequest
{
    public const string Route = "/klinik/patients/{patientId}/medical-records/{medicalRecordId}";

    public Guid PatientId { get; set; }
    public Guid MedicalRecordId { get; set; }
    public DateOnly VisitDate { get; set; }
    public string Diagnosis { get; set; } = null!;
    public string Therapy { get; set; } = null!;
    public string? Notes { get; set; }
}