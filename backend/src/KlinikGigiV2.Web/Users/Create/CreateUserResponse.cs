using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.Web.Models;

namespace KlinikGigiV2.Web.Users.Create;

public class CreateUserResponse : GenericResponse
{
  public UserRecord? User { get; set; }
}
