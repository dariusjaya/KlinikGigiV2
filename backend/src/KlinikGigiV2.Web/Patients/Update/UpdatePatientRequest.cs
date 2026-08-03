using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.Patients.Update;

public class UpdatePatientRequest
{
    public const string Route = "/klinik/patients/{PatientId}";

    public Guid PatientId { get; set; }
    public string FullName { get; set; } = null!;
    public DateOnly? birthDate { get; set; }
    public string? Occupation { get; set; }
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
