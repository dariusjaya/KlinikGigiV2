using Ardalis.Specification;
using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.Core.PatientAggregate.Specifications;
using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core;
namespace KlinikGigiV2.UseCases.Patients.Get;

public class GetPatientHandler(IRepository<Patient> repository) : IQueryHandler<GetPatientQuery, Result<PatientRecord>>
{


    public async ValueTask<Result<PatientRecord>> Handle(GetPatientQuery req, CancellationToken ct)
    {
        var patient = await repository.GetByIdAsync(req.PatientId, ct);

        if (patient is null)
        {
            return Result.Error("Patient not found.");
        }

        var patientRecord = PatientRecord.MapFromPatient(patient);

        return Result.Success(patientRecord, "Patient retrieved successfully.");
    }
}

