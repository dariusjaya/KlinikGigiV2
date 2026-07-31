namespace KlinikGigiV2.Core.UserAggregate;

public record UserRecord(
  Guid Id,
  string? Email,
  string? Role,
  string? FullName,
  DateTime CreatedAt,
  DateTime UpdatedAt
)
{
  public static UserRecord MapFromUser(User user) => new UserRecord(
    Id: user.Id,
    Email: user.Email,
    Role: user.Role,
    FullName: user.FullName,
    CreatedAt: user.CreatedAt,
    UpdatedAt: user.UpdatedAt
  );
}
