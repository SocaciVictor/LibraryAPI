namespace LibraryAPI.Core.Models
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
