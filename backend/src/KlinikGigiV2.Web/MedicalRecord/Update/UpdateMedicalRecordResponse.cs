using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.MedicalRecords.Update;

public class UpdateMedicalRecordResponse : GenericResponse
{
    public MedicalRecordRecord? MedicalRecord { get; set; }
}