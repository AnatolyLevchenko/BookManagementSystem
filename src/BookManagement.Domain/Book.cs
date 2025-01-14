namespace BookManagement.Domain;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Year { get; set; }

    public virtual  Genre Genre { get; set; } = null!;
    public int GenreId { get; set; }
    public virtual  Author Author { get; set; } = null!;
    public int AuthorId { get; set; }
}