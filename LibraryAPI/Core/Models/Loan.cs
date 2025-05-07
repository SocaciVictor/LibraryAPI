using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
