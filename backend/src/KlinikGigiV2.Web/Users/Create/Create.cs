using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.UseCases.Users.Create;

namespace KlinikGigiV2.Web.Users.Create;

public class Create(
  IMediator _mediator
) : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post(CreateUserRequest.Route);
        AllowAnonymous();

    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(
          FullName: req.FullName,
          Email: req.Email,
          Password: req.PasswordHash
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new CreateUserResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 400, ct);
            return;
        }

        await Send.OkAsync(new CreateUserResponse
        {
            User = UserRecord.MapFromUser(result.Value),
            Message = "User created successfully."
        }, ct);
    }
}
