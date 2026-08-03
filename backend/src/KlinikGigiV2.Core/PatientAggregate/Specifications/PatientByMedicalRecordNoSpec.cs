namespace KlinikGigiV2.Core.PatientAggregate.Specifications;

public class PatientByMedicalRecordNoSpec : Specification<Patient>
{
    public PatientByMedicalRecordNoSpec(string medicalRecordNo)
    {
        Query.Where(x => x.MedicalRecordNo == medicalRecordNo);
    }
}

