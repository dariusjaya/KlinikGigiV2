using ErrorOr;
using KlinikGigiV2.Core.UserAggregate;

namespace KlinikGigiV2.UseCases.Users.Create;

public record CreateUserCommand
(
  string FullName,
  string Email,
  string Password
) : ICommand<ErrorOr<User>>;

