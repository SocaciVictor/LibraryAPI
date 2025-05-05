namespace LibraryAPI.Infrastructure.Entities;

public class BookDb
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public int Quantity { get; set; }
    public required AuthorDb Author { get; set; }

}
