using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.MedicalRecords.Get;

public class GetMedicalRecordResponse : GenericResponse
{
    public MedicalRecordRecord? MedicalRecord { get; set; }
}
