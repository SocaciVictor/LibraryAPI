namespace LibraryAPI.Core.Models
{
    public class FilterBook
    {
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
