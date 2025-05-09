namespace LibraryAdminPage.Components.Models
{
    public record LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}
