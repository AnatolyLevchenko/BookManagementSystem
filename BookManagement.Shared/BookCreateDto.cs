namespace BookManagement.Shared;

public class BookCreateDto
{
    public required string Title { get; set; }
    public int Year { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}