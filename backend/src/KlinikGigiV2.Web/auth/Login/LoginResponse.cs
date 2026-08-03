using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.Auth.Login;

public class LoginResponse : GenericResponse
{
    public string? Token { get; set; }
    public UserRecord? User { get; set; }
}