using ErrorOr;
using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.UseCases.Patients.Update;

public record UpdatePatientCommand
(
    Guid PatientId,
    string FullName,
    DateOnly? BirthDate,
    string? Occupation,
    string Address,
    string Phone
) : ICommand<ErrorOr<Patient>>;

