using FastEndpoints;
using KlinikGigiV2.Web.Models;
using KlinikGigiV2.UseCases.MedicalRecords.Get;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.Web.MedicalRecords.Get;

public class Get(IMediator _mediator) : Endpoint<GetMedicalRecordRequest, GetMedicalRecordResponse>
{
    public override void Configure()
    {
        Get(GetMedicalRecordRequest.Route);

    }

    public override async Task HandleAsync(GetMedicalRecordRequest req, CancellationToken ct)
    {
        var query = new GetMedicalRecordQuery(req.PatientId, req.MedicalRecordId);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new GetMedicalRecordResponse
            {
                Message = ErrorValidation.GetErrorMessage(result),
                Error = ErrorValidation.UpdateErrorList(result)
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new GetMedicalRecordResponse
        {
            MedicalRecord = result.Value,
            Message = "Medical record retrieved successfully."
        }, ct);
        return;
    }
}
