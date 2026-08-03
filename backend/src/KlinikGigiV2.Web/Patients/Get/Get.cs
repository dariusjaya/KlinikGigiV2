using FastEndpoints;
using KlinikGigiV2.Web.Models;
using KlinikGigiV2.UseCases.Patients.Get;
using KlinikGigiV2.Core.PatientAggregate;

namespace KlinikGigiV2.Web.Patients.Get;

public class Get(IMediator _mediator) : Endpoint<GetPatientRequest, GetPatientResponse>
{
    public override void Configure()
    {
        Get(GetPatientRequest.Route);


    }

    public override async Task HandleAsync(GetPatientRequest req, CancellationToken ct)
    {
        var query = new GetPatientQuery(req.PatientId);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new GetPatientResponse
            {
                Message = ErrorValidation.GetErrorMessage(result),
                Error = ErrorValidation.UpdateErrorList(result)
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new GetPatientResponse
        {
            Patient = result.Value,
            Message = "Patient retrieved successfully."
        }, ct);
        return;
    }
}
