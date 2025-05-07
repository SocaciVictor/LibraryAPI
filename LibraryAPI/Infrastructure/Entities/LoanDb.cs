namespace LibraryAPI.Infrastructure.Entities
{
    public class LoanDb
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required UserDb User { get; set; }
        public int BookId { get; set; }
        public required BookDb Book { get; set; }
        public DateTime LoanedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}