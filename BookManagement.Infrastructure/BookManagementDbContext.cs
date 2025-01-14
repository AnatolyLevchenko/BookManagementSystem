using BookManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure;

public class BookManagementDbContext(DbContextOptions<BookManagementDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
}
