using BookManagement.Domain;
namespace BookManagement.Infrastructure.Seed;

public class SeedData
{
    public List<Author> Authors { get; set; } = [];
    public List<Genre> Genres { get; set; } = [];
    public List<Book> Books { get; set; } = [];
}