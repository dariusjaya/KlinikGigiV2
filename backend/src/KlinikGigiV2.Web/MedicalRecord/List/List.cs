using FastEndpoints;
using KlinikGigiV2.Web.Models;
using KlinikGigiV2.UseCases.MedicalRecords.List;

namespace KlinikGigiV2.Web.MedicalRecords.List;

/// <summary>
/// List all Medical Records
/// </summary>
/// <remarks>
/// List all medical records for a specific patient - returns MedicalRecordListResponse within the application.
/// </remarks>
public class List(IMediator _mediator) : Endpoint<ListMedicalRecordRequest, ListMedicalRecordResponse>
{
    public override void Configure()
    {
        Get(ListMedicalRecordRequest.Route);

    }

    public override async Task HandleAsync(ListMedicalRecordRequest req, CancellationToken ct)
    {
        var query = new ListMedicalRecordQuery(req.PatientId, req.Search, req.Page, req.PageSize);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new ListMedicalRecordResponse
            {
                Message = ErrorValidation.GetErrorMessage(result),
                Error = ErrorValidation.UpdateErrorList(result)
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new ListMedicalRecordResponse
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
