using Ardalis.Specification;
using ErrorOr;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.PatientAggregate.Specifications;
namespace KlinikGigiV2.UseCases.Patients.Create;

public class CreatePatientHandler(
    IRepository<Patient> patientRepository)
    : ICommandHandler<CreatePatientCommand, ErrorOr<Patient>>
{


    public async ValueTask<ErrorOr<Patient>> Handle(
        CreatePatientCommand request,
        CancellationToken ct)
    {
        var existingPatient = await patientRepository.FirstOrDefaultAsync(
            new PatientByMedicalRecordNoSpec(request.MedicalRecordNo),
            ct);

        if (existingPatient is not null)
        {
            return Error.Conflict(
                code: "PATIENT_MEDICAL_RECORD_NO_ALREADY_EXISTS",
                description: "A patient with this medical record number already exists.");
        }

        var patient = Patient.Create(
            request.MedicalRecordNo,
            request.FullName,
            request.BirthDate,
            request.Occupation,
            request.Address,
            request.Phone
        );

        await patientRepository.AddAsync(patient, ct);

        return patient;
    }
}
