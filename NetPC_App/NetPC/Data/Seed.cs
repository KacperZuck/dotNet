using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetPC.Models;

namespace NetPC.Data
{
    public static class SeedData
    {
        public static async Task Initialize(DBContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Contacts.AnyAsync())
            {
                await context.Contacts.AddRangeAsync(
                    new Contact
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        Surname = "admin",
                        Email = "admin@test.pl",
                        Password = "Haslo123",
                        Phone = 123456789,
                        Category = Categories.Prywatny,
                        Birth_Date = new DateOnly(1999, 1, 1),
                        Company = null,
                        OtherCategory = null
                    },
                    new Contact
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jan",
                        Surname = "Kowalski",
                        Email = "jan@test.pl",
                        Password = "SDFA12sfgaa",
                        Phone = 123456789,
                        Category = Categories.Prywatny,
                        Birth_Date = new DateOnly(1999, 1, 1),
                        Company = null,
                        OtherCategory = null
                    },
                    new Contact
                    {
                        Id = Guid.NewGuid(),
                        Name = "Anna",
                        Surname = "Nowak",
                        Email = "anna@test.pl",
                        Password = "DAF123jawa",
                        Phone = 555555555,
                        Category = Categories.Prywatny,
                        Birth_Date = new DateOnly(2000, 5, 10),
                        Company = null,
                        OtherCategory = null
                    }
                );

                await context.SaveChangesAsync();
            }

        }

    }
}