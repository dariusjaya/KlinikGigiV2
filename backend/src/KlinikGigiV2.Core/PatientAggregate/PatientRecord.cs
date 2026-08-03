namespace KlinikGigiV2.Core.PatientAggregate;

public record PatientRecord(
  Guid Id,
  string MedicalRecordNo,
  string FullName,
  DateOnly? BirthDate,
  string? Occupation,
  string Address,
  string Phone
)
{
    public static PatientRecord MapFromPatient(Patient patient) => new PatientRecord(
      Id: patient.Id,
      MedicalRecordNo: patient.MedicalRecordNo,
      FullName: patient.FullName,
      BirthDate: patient.BirthDate,
      Occupation: patient.Occupation,
      Address: patient.Address,
      Phone: patient.Phone
    );
}


