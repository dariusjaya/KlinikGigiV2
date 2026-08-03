namespace KlinikGigiV2.Web.Patients.Create;

public class CreatePatientRequest
{
    public const string Route = "/klinik/patients";

    public string MedicalRecordNo { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateOnly? birthDate { get; set; }
    public string? Occupation { get; set; }
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
