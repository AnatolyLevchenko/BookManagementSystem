namespace BookManagement.Shared;

public class BookReadDto
{
    public  required int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public int Year { get; set; }
    public required string Genre { get; set; }
}