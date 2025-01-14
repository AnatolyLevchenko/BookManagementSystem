using BookManagement.Domain;
using BookManagement.Infrastructure;
using BookManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Services;

public class BookService(BookManagementDbContext dbContext)
{
    public Task<List<BookReadDto>> GetAllBooks()
    {
        return dbContext.Books
            .Select(x => new BookReadDto
            {
                Author = x.Author.Name,
                Title = x.Title,
                Year = x.Year,
                Genre = x.Genre.Name
            })
            .ToListAsync();
    }

    public Task<BookReadDto?> GetBookById(int id)
    {
        return dbContext.Books
            .Where(x => x.Id == id)
            .Select(x => new BookReadDto
            {
                Author = x.Author.Name,
                Title = x.Title,
                Year = x.Year,
                Genre = x.Genre.Name
            })
            .FirstOrDefaultAsync();

    }

    public async Task<Book> CreateBook(BookCreateDto book)
    {
        var entity = new Book
        {
            Title = book.Title,
            Year = book.Year,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId
        };
        await dbContext.Books.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateBook(int id, Book updatedBook)
    {
        var existingBook = await dbContext.Books.FindAsync(id);
        if (existingBook != null)
        {
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Year = updatedBook.Year;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteBook(int id)
    {
        var book = await dbContext.Books.FindAsync(id);
        if (book != null)
        {
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }
    }
}