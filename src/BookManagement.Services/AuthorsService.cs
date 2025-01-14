using BookManagement.Infrastructure;
using BookManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Services;

public class AuthorsService(BookManagementDbContext dbContext)
{
    public async Task<List<AuthorDto>> GetAllAuthors()
    {
        return await dbContext.Authors.Select(x => new AuthorDto
        {
            Name = x.Name,
            Id = x.Id
        }).ToListAsync();
    }
}