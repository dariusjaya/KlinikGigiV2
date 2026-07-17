using Ardalis.Specification;
using ErrorOr;
using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.Core.UserAggregate.Specifications;
namespace KlinikGigiV2.UseCases.Users.Create;

public class CreateUserHandler(
    IRepository<User> userRepository,
    IPasswordHasher passwordHasher)
    : ICommandHandler<CreateUserCommand, ErrorOr<User>>
{


    public async ValueTask<ErrorOr<User>> Handle(
        CreateUserCommand request,
        CancellationToken ct)
    {
        var existingUser = await userRepository.FirstOrDefaultAsync(
            new UserByEmailSpec(request.Email),
            ct);

        if (existingUser is not null)
        {
            return Error.Conflict(
                code: "USER_EMAIL_ALREADY_EXISTS",
                description: "A user with this email already exists.");
        }

        var passwordHash = passwordHasher.HashPassword(request.Password);

        var user = User.Create(
            request.FullName,
            request.Email,
            passwordHash);

        await userRepository.AddAsync(user, ct);

        return user;
    }
}