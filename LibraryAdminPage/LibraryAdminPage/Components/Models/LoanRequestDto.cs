namespace LibraryAdminPage.Components.Models
{
    public record LoanRequestDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
