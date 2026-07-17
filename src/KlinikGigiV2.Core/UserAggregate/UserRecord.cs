namespace KlinikGigiV2.Core.UserAggregate;

/// <summary>
/// UserRecord - Only used for /me purposes (for the user itself to consume)
/// never meant to be shown to another users
/// <br />
/// <br />
/// NOTE: USE THIS ONLY ON APPLICATION LAYER.
/// <br />
/// YOU MIGHT STILL NEED TO CREATE ANOTHER RECORD OR DTO 
/// <br />
/// USER RECORD IS ONLY ONE, THERE'S NEVER BEEN MORE.
/// <br />
/// IF YOU HAVE YOUR OWN "SPECIFIC PURPOSE"
/// SUCH AS COMBING A USER RECORD WITH USER ROLE RECORD,
/// <br />
/// THEN YOU MUST CREATE A NEW ONE, WITH A CLEAR NAME.
/// TO AVOID CONFUSION... !!!
/// </summary>
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
