﻿namespace LibraryAPI.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
