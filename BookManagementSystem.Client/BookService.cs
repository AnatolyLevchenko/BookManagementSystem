using System.Net.Http.Json;
using BookManagement.Shared;

namespace BookManagementSystem.Client;
public class BookService(HttpClient httpClient)
{
    public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
    {
        return (await httpClient.GetFromJsonAsync<IEnumerable<BookReadDto>>("api/books"))!;
    }

    public async Task<BookReadDto> GetBookByIdAsync(int id)
    {
        return (await httpClient.GetFromJsonAsync<BookReadDto>($"api/books/{id}"))!;
    }

    public async Task CreateBookAsync(BookCreateDto book)
    {
        await httpClient.PostAsJsonAsync("api/books", book);
    }

    public async Task UpdateBookAsync(int id, BookCreateDto book)
    {
        await httpClient.PutAsJsonAsync($"api/books/{id}", book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await httpClient.DeleteAsync($"api/books/{id}");
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        return (await httpClient.GetFromJsonAsync<IEnumerable<AuthorDto>>("api/authors"))!;
    }

    public async Task<IEnumerable<GenreDto>> GetGenresAsync()
    {
        return (await httpClient.GetFromJsonAsync<IEnumerable<GenreDto>>("api/genres"))!;
    }
}