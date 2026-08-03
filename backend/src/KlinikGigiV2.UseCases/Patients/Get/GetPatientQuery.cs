using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core.PatientAggregate;


namespace KlinikGigiV2.UseCases.Patients.Get;

public record GetPatientQuery(Guid PatientId) : IQuery<Result<PatientRecord>>;
