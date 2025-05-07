namespace LibraryAPI.Infrastructure.Entities
{
    public class UserDb
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
