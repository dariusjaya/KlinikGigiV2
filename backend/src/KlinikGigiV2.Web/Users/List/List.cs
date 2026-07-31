using FastEndpoints;
using KlinikGigiV2.Web.Models;
using KlinikGigiV2.UseCases.Users.List;

namespace KlinikGigiV2.Web.Users.List;

/// <summary>
/// List all User
/// </summary>
/// <remarks>
/// List all accounts - returns UserListResponse within the application.
/// </remarks>
public class List(IMediator _mediator) : Endpoint<ListUserRequest, ListUserResponse>
{
    public override void Configure()
    {
        Get(ListUserRequest.Route);
        AllowAnonymous();

    }

    public override async Task HandleAsync(ListUserRequest req, CancellationToken ct)
    {
        var query = new ListUserQuery(req.Search, req.Page, req.PageSize);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            await Send.ResponseAsync(new ListUserResponse
            {
                Message = ErrorValidation.GetErrorMessage(result),
                Error = ErrorValidation.UpdateErrorList(result)
            }, 500, ct);
            return;
        }

        await Send.OkAsync(new ListUserResponse
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
