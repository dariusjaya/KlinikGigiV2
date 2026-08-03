using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.MedicalRecords.Create;

public class CreateMedicalRecordResponse : GenericResponse
{
    public MedicalRecordRecord? MedicalRecord { get; set; }
}
