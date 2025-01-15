using BookManagement.Domain;
using BookManagement.Infrastructure;
using BookManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Services;

public class BookService(BookManagementDbContext dbContext)
{
    public Task<BookReadDto?> GetBookById(int id)
    {
        return dbContext.Books
            .Where(x => x.Id == id)
            .Select(x => new BookReadDto
            {
                Id = x.Id,
                Author = x.Author.Name,
                Title = x.Title,
                Year = x.Year,
                Genre = x.Genre.Name,
                AuthorId = x.AuthorId,
                GenreId = x.GenreId
            })
            .FirstOrDefaultAsync();

    }

    public async Task<BookReadDto> CreateBook(BookCreateDto book)
    {
        var createdBook = new Book
        {
            Title = book.Title,
            Year = book.Year,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId
        };
        await dbContext.Books.AddAsync(createdBook);
        await dbContext.SaveChangesAsync();

        var bookWithRelations = await dbContext.Books.
            Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstAsync(b => b.Id == createdBook.Id);

        var bookReadDto = new BookReadDto
        {
            Id = bookWithRelations.Id,
            Title = bookWithRelations.Title,
            Author = bookWithRelations.Author.Name,
            Year = bookWithRelations.Year,
            Genre = bookWithRelations.Genre.Name,
            AuthorId = bookWithRelations.AuthorId,
            GenreId = bookWithRelations.GenreId
        };
        return bookReadDto;
    }

    public async Task UpdateBook(int id, BookUpdateDto updatedBook)
    {
        var existingBook = await dbContext.Books.FindAsync(id);
        if (existingBook != null)
        {
            existingBook.Title = updatedBook.Title;
            existingBook.AuthorId = updatedBook.AuthorId;
            existingBook.GenreId = updatedBook.GenreId;
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

    public async Task<object?> GetPagedList(int page, int pageSize, string? searchTerm)
    {
        var query = dbContext.Books.AsQueryable();

        // Apply search term if provided
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(b => b.Title.Contains(searchTerm));
        }

        // Count the total number of items that match the query
        var totalItems = await query.CountAsync();

        // Apply sorting to ensure consistent data order across pages
        query = query.OrderBy(b => b.Title);

        // Apply pagination
        var books = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BookReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                Genre = b.Genre.Name,
                Year = b.Year,
                AuthorId = b.AuthorId,
                GenreId = b.GenreId
            })
            .ToListAsync();

        // Return paginated result
        return new PagedResult<BookReadDto>
        {
            Items = books,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }

}