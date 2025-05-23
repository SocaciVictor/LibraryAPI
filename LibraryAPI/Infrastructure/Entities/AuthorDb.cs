﻿namespace LibraryAPI.Infrastructure.Entities
{
    public class AuthorDb
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<BookDb> Books { get; set; }

        public bool IsDeleted { get; set; }
    }
}