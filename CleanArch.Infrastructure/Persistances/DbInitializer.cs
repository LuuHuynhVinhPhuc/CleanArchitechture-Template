namespace CleanArch.Infrastructure.Persistances
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // The database schema is already created by MigrateAsync before this runs.
            // Seed data for each entity here.
            // Example:
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
