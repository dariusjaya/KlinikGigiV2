
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.Core.UserAggregate;

public class User : EntityBase<Guid>, IAggregateRoot, IAuditableEntity, IHasDomainEvents
{
    public string FullName { get; private set; } = null!;   // Nama perawat/dokter
    public string Email { get; private set; } = null!;     // Untuk login
    public string PasswordHash { get; private set; } = null!;
    public string Role { get; private set; } = UserRoleEnum.Perawat;      // "doctor" / "nurse"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Track siapa yang input rekam medis
    public ICollection<MedicalRecord> MedicalRecords { get; private set; } = [];

    public static User Create(
        string fullName,
        string email,
        string passwordHash)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = Guard.Against.NullOrWhiteSpace(fullName),
            Email = Guard.Against.NullOrWhiteSpace(email).Trim().ToLowerInvariant(),
            PasswordHash = Guard.Against.NullOrWhiteSpace(passwordHash),
        };



        return user;
    }

    public void UpdateFullName(string fullName)
    {
        FullName = Guard.Against.NullOrWhiteSpace(fullName);


    }

    public void UpdateEmail(string email)
    {
        Email = Guard.Against.NullOrWhiteSpace(email);


    }

    public void UpdateRole(string role)
    {
        Role = Guard.Against.NullOrWhiteSpace(role);


    }



}