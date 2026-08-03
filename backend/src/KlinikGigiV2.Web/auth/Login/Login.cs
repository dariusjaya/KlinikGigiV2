using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.UseCases.Auth.Login;

namespace KlinikGigiV2.Web.Auth.Login;

public class Login(IMediator _mediator) : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post(LoginRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var command = new LoginCommand(
            Email: req.Email,
            Password: req.Password
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new LoginResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 401, ct);
            return;
        }

        await Send.OkAsync(new LoginResponse
        {
            Token = result.Value.Token,
            Message = "Login berhasil.",
            User = new UserRecord(
                Id: result.Value.UserId,
                Email: result.Value.Email,
                Role: result.Value.Role,
                FullName: result.Value.FullName,
                CreatedAt: DateTime.UtcNow,
                UpdatedAt: DateTime.UtcNow
            )
        }, ct);
    }
}