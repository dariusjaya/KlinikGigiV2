namespace KlinikGigiV2.Core.UserAggregate.Specifications;

public class UserByEmailSpec : Specification<User>
{
    public UserByEmailSpec(string email)
    {
        Query.Where(x => x.Email == email);
    }
}