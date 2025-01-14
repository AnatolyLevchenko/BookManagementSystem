using Newtonsoft.Json;

namespace BookManagement.Infrastructure.Seed;

public static class DbMigrator
{
    public static void Migrate(BookManagementDbContext dbContext)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData.json");

        if (!File.Exists(filePath))
        {
            throw new Exception("File with seed data not found! Cannot proceed with migration.");
        }

        var jsonData = File.ReadAllText(filePath);
        var seedData = JsonConvert.DeserializeObject<SeedData>(jsonData);

        if (seedData == null)
        {
            throw new Exception("Seed data deserialization failed.");
        }

        if (!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(seedData.Genres);
        }

        if (!dbContext.Authors.Any())
        {
            dbContext.Authors.AddRange(seedData.Authors);
        }

        if (!dbContext.Books.Any())
        {
            dbContext.Books.AddRange(seedData.Books);
        }
        dbContext.SaveChanges();
    }
}