namespace KlinikGigiV2.Web.MedicalRecords.Get;

public class GetMedicalRecordRequest
{
    public const string Route = "/klinik/patients/{patientId}/medical-records/{medicalRecordId}";
    public Guid PatientId { get; set; }

    public Guid MedicalRecordId { get; set; }
}


