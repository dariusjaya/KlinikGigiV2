using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.Patients.Update;

public class UpdatePatientResponse : GenericResponse
{
    public PatientRecord? patient { get; set; }
}
