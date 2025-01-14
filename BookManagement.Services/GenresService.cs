using BookManagement.Infrastructure;
using BookManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Services;

public class GenresService(BookManagementDbContext dbContext)
{
    public async Task<List<GenreDto>?> GetAllGenres()
    {
        return await dbContext.Genres.Select(x => new GenreDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();
    }
}