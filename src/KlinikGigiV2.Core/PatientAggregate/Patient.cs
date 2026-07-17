using Ardalis.GuardClauses;
using KlinikGigiV2.Core.MedicalRecordAggregate;

namespace KlinikGigiV2.Core.PatientAggregate;

public class Patient : EntityBase<Guid>, IAggregateRoot, IAuditableEntity
{
    public string MedicalRecordNo { get; private set; } = null!;
    public string FullName { get; private set; } = null!;
    public DateOnly? BirthDate { get; private set; }
    public string? Occupation { get; private set; }
    public string Address { get; private set; } = null!;
    public string Phone { get; private set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MedicalRecord> MedicalRecords { get; private set; } = [];

    public static Patient Create(
        string medicalRecordNo,
        string fullName,
        DateOnly? birthDate,
        string? occupation,
        string address,
        string phone)
    {
        return new Patient
        {
            Id = Guid.NewGuid(),
            MedicalRecordNo = Guard.Against.NullOrWhiteSpace(medicalRecordNo),
            FullName = Guard.Against.NullOrWhiteSpace(fullName),
            BirthDate = birthDate,
            Occupation = occupation,
            Address = Guard.Against.NullOrWhiteSpace(address),
            Phone = Guard.Against.NullOrWhiteSpace(phone)
        };
    }

    public void Update(
        string fullName,
        DateOnly? birthDate,
        string? occupation,
        string address,
        string phone)
    {
        FullName = Guard.Against.NullOrWhiteSpace(fullName);
        BirthDate = birthDate;
        Occupation = occupation;
        Address = Guard.Against.NullOrWhiteSpace(address);
        Phone = Guard.Against.NullOrWhiteSpace(phone);

        UpdatedAt = DateTime.UtcNow;
    }
}