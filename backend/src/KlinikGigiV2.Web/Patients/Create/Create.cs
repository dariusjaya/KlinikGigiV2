using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.UseCases.Patients.Create;

namespace KlinikGigiV2.Web.Patients.Create;

public class Create(
  IMediator _mediator
) : Endpoint<CreatePatientRequest, CreatePatientResponse>
{
    public override void Configure()
    {
        Post(CreatePatientRequest.Route);
    }

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        var command = new CreatePatientCommand(
          MedicalRecordNo: req.MedicalRecordNo,
          FullName: req.FullName,
          BirthDate: req.birthDate,
          Occupation: req.Occupation,
          Address: req.Address,
          Phone: req.Phone
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new CreatePatientResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 400, ct);
            return;
        }

        await Send.OkAsync(new CreatePatientResponse
        {
            Patient = PatientRecord.MapFromPatient(result.Value),
            Message = "Patient created successfully."
        }, ct);
    }
}
