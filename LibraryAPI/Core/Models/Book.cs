using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int AuthorId { get; set; }
        public int Quantity { get; set; }
        public List<int> Loans { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
