namespace BookManagement.Shared;

public class BookCreateDto
{
    public  string Title { get; set; } = null!;
    public int Year { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}