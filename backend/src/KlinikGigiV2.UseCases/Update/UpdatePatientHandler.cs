using Ardalis.Specification;
using ErrorOr;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.PatientAggregate.Specifications;
namespace KlinikGigiV2.UseCases.Patients.Update;

public class UpdatePatientHandler(
    IRepository<Patient> patientRepository)
    : ICommandHandler<UpdatePatientCommand, ErrorOr<Patient>>
{


    public async ValueTask<ErrorOr<Patient>> Handle(
    UpdatePatientCommand request,
    CancellationToken ct)
    {
        var existingPatient = await patientRepository.GetByIdAsync(request.PatientId, ct);

        if (existingPatient is null)
        {
            return Error.NotFound(
                code: "PATIENT_NOT_FOUND",
                description: "Patient not found.");
        }

        existingPatient.Update(
            request.FullName,
            request.BirthDate,
            request.Occupation,
            request.Address,
            request.Phone
        );

        await patientRepository.UpdateAsync(existingPatient, ct);

        return existingPatient;
    }
}
