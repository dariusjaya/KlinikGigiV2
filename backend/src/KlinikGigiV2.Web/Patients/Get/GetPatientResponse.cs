using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.Patients.Get;

public class GetPatientResponse : GenericResponse
{
    public PatientRecord? Patient { get; set; }
}
