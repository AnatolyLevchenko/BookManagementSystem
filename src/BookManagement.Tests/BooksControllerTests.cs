using System.Net.Http.Json;
using BookManagement.Shared;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookManagement.Tests;

public class BooksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public BooksControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetPagedList_ReturnsOk()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;
        var searchTerm = string.Empty;

        // Act
        var response = await _client.GetAsync($"/api/books?page={page}&pageSize={pageSize}&searchTerm={searchTerm}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenBookExists()
    {
        // Arrange
        var bookId = 1;

        // Act
        var response = await _client.GetAsync($"/api/books/{bookId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var book = await response.Content.ReadFromJsonAsync<BookReadDto>();
        Assert.NotNull(book);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = 999;

        // Act
        var response = await _client.GetAsync($"/api/books/{bookId}");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsCreated()
    {
        // Arrange
        var newBook = new BookCreateDto
        {
            Title = "New Book",
            AuthorId = 1,
            GenreId = 2,
            Year = 2025,
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/books", newBook);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdBook = await response.Content.ReadFromJsonAsync<BookReadDto>();
        Assert.NotNull(createdBook);
        Assert.Equal(newBook.Title, createdBook?.Title);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenBookExists()
    {
        // Arrange
        var bookId = 1;
        var updatedBook = new BookUpdateDto
        {
            Title = "Updated Title",
            AuthorId = 3,
            GenreId = 2,
            Year = 2026
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/books/{bookId}", updatedBook);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenBookExists()
    {
        // Arrange
        var bookId = 1;

        // Act
        var response = await _client.DeleteAsync($"/api/books/{bookId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
}