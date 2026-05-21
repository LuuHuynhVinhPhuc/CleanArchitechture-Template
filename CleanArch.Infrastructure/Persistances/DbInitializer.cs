using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infrastructure.Persistances
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ki?m tra xem database dã du?c t?o chua (th?c t? MigrateAsync dã làm vi?c này)

            // Th?c hi?n Seed Data cho t?ng Entity ? dây
            // Ví d?:
            /*
            if (!await context.Users.AnyAsync())
            {
                context.Users.Add(new User { Name = "Admin" });
                await context.SaveChangesAsync();
            }
            */

            await Task.CompletedTask;
        }
    }
}
