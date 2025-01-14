namespace BookManagement.Shared;

public class BookUpdateDto
{
    public required string Title { get; set; }
    public int Year { get; set; }
    public required int AuthorId { get; set; }
    public int GenreId { get; set; }
}