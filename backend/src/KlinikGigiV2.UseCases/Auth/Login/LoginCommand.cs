using ErrorOr;

namespace KlinikGigiV2.UseCases.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
) : ICommand<ErrorOr<LoginResult>>;

public record LoginResult(string Token, Guid UserId, string FullName, string Email, string Role);