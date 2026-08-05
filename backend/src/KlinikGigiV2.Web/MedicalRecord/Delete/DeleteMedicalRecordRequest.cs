namespace KlinikGigiV2.Web.MedicalRecords.Delete;

public class DeleteMedicalRecordRequest
{
    public const string Route = "/klinik/patients/{patientId}/medical-records/{medicalRecordId}";

    public Guid PatientId { get; set; }
    public Guid MedicalRecordId { get; set; }
}