using ErrorOr;
using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.UseCases.Patients.Create;

public record CreatePatientCommand
(
    string MedicalRecordNo,
    string FullName,
    DateOnly? BirthDate,
    string? Occupation,
    string Address,
    string Phone
) : ICommand<ErrorOr<Patient>>;

