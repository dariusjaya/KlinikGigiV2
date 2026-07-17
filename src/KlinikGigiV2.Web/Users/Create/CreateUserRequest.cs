namespace KlinikGigiV2.Web.Users.Create;

public class CreateUserRequest
{
  public const string Route = "/klinik/users";

  public string FullName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;
}
