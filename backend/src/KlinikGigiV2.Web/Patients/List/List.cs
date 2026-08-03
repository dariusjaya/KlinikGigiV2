using FastEndpoints;
using KlinikGigiV2.Web.Models;
using KlinikGigiV2.UseCases.Patients.List;

namespace KlinikGigiV2.Web.Patients.List;

/// <summary>
/// List all Patient
/// </summary>
/// <remarks>
/// List all patients - returns PatientListResponse within the application.
/// </remarks>
public class List(IMediator _mediator) : Endpoint<ListPatientRequest, ListPatientResponse>
{
    public override void Configure()
    {
        Get(ListPatientRequest.Route);


    }

    public override async Task HandleAsync(ListPatientRequest req, CancellationToken ct)
    {
        var query = new ListPatientQuery(req.Search, req.Page, req.PageSize);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new ListPatientResponse
            {
                Message = ErrorValidation.GetErrorMessage(result),
                Error = ErrorValidation.UpdateErrorList(result)
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new ListPatientResponse
        {
            Items = result.Value.Items,
            TotalItems = result.Value.TotalItems,
            Page = result.Value.Page,
            PageSize = result.Value.PageSize,
            Message = result.SuccessMessage
        }, ct);
        return;
    }
}
