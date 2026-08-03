using KlinikGigiV2.Core.PatientAggregate;
using KlinikGigiV2.UseCases.Patients.Update;

namespace KlinikGigiV2.Web.Patients.Update;

public class Update(IMediator _mediator) : Endpoint<UpdatePatientRequest, UpdatePatientResponse>
{
    public override void Configure()
    {
        Put(UpdatePatientRequest.Route);

    }

    public override async Task HandleAsync(UpdatePatientRequest req, CancellationToken ct)
    {

        var command = new UpdatePatientCommand
        (
          PatientId: req.PatientId,
          FullName: req.FullName,
          BirthDate: req.birthDate,
          Occupation: req.Occupation,
          Address: req.Address,
          Phone: req.Phone
        );
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new UpdatePatientResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new UpdatePatientResponse
        {
            patient = PatientRecord.MapFromPatient(result.Value),
            Message = "Patient updated successfully."
        }, ct);
        return;
    }
}
