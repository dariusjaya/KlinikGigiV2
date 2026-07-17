using KlinikGigiV2.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KlinikGigiV2.Infrastructure.Data;

public static class SeedData
{
  public static async Task InitializeAsync(AppDbContext dbContext)
  {
    if (await dbContext.Users.AnyAsync())
      return;

    await PopulateTestDataAsync(dbContext);
  }

  public static async Task PopulateTestDataAsync(AppDbContext dbContext)
  {
    var perawat = User.Create(
        fullName: "Administrator",
        email: "admin@klinik.com",
        passwordHash: "Admin123!"
    );

    perawat.UpdateRole(UserRoleEnum.Perawat);

    var doctor = User.Create(
        fullName: "Dr. Andi",
        email: "doctor@klinik.com",
        passwordHash: "Doctor123!"
    );

    doctor.UpdateRole(UserRoleEnum.Dokter);




    dbContext.Users.AddRange(perawat, doctor);

    await dbContext.SaveChangesAsync();
  }
}