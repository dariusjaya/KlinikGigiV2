using ErrorOr;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;
using KlinikGigiV2.Core.UserAggregate.Specifications;
namespace KlinikGigiV2.UseCases.Auth.Login;

public class LoginHandler(
    IReadRepository<Core.UserAggregate.User> userRepository,
    IPasswordHasher passwordHasher,
    IConfiguration configuration)
    : ICommandHandler<LoginCommand, ErrorOr<LoginResult>>
{
    public async ValueTask<ErrorOr<LoginResult>> Handle(
        LoginCommand request,
        CancellationToken ct)
    {
        var user = await userRepository.FirstOrDefaultAsync(
            new UserByEmailSpec(request.Email),
            ct);

        if (user is null)
        {
            return Error.Unauthorized(
                code: "INVALID_CREDENTIALS",
                description: "Email atau password salah.");
        }

        var isPasswordValid = passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return Error.Unauthorized(
                code: "INVALID_CREDENTIALS",
                description: "Email atau password salah.");
        }

        var token = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = configuration["Jwt:Key"]!;
            o.ExpireAt = DateTime.UtcNow.AddHours(8);
            o.User.Claims.Add(("sub", user.Id.ToString()));
            o.User.Claims.Add(("email", user.Email));
            o.User.Claims.Add(("role", user.Role));
            o.User.Claims.Add(("fullName", user.FullName));
        });

        return new LoginResult(token, user.Id, user.FullName, user.Email, user.Role);
    }
}