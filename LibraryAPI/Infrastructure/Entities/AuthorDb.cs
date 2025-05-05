namespace LibraryAPI.Infrastructure.Entities
{
    public class AuthorDb
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public  required string LastName { get; set; }

        public List<BookDb> Books { get; set; } = new List<BookDb>();
    }
}