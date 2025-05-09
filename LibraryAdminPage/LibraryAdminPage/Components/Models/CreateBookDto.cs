namespace LibraryAdminPage.Components.Models
{
    public record CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int Quantity { get; set; }
        public DateTime PublishedDate { get; set; }
    }

}
