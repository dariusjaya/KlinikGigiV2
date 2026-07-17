using Ardalis.SmartEnum;

namespace KlinikGigiV2.Core.UserAggregate;

public sealed class UserRoleEnum : SmartEnum<UserRoleEnum, string>
{
    public static readonly UserRoleEnum Dokter = new(nameof(Dokter), "dokter");
    public static readonly UserRoleEnum Perawat = new(nameof(Perawat), "perawat");

    private UserRoleEnum(string name, string value) : base(name, value) { }
}
