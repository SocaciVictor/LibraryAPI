using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AuthorDb>()
                .HasQueryFilter(a => !a.IsDeleted);

            modelBuilder.Entity<BookDb>()
                .HasQueryFilter(b => !b.IsDeleted);

            modelBuilder.Entity<LoanDb>()
                .HasQueryFilter(l => !l.IsDeleted);


         
            modelBuilder.Entity<BookDb>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanDb>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<BookDb> Books { get; set; }
        public DbSet<AuthorDb> Authors { get; set; }
        public DbSet<LoanDb> Loans { get; set; }
    }
}
