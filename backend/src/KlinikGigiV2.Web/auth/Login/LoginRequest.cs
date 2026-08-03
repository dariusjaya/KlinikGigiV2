namespace KlinikGigiV2.Web.Auth.Login;

public class LoginRequest
{
    public const string Route = "/klinik/auth/login";

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}