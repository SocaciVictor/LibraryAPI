using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class BookRepository(LibraryDbContext _db, IMapper _mapper) : IBookRepository
    {
        public async Task<Book> AddAsync(Book b) 
        {
            var bookDb = _mapper.Map<BookDb>(b);
            await _db.Books.AddAsync(bookDb); 
            await _db.SaveChangesAsync(); 
            var createdBook = _mapper.Map<Book>(bookDb);

            return createdBook;
        }
        public async Task<Book?> GetByIdAsync(int id)
        {
            var bookDb = await _db.Books.FindAsync(id);
            var book = _mapper.Map<Book>(bookDb);

            return book;
        }

        public async Task<Book?> GetByTitleAsync(string title, bool isDeleted = false)
        {
            var bookDb = await _db.Books.FirstOrDefaultAsync(b => b.Title.Equals(title) && b.IsDeleted == isDeleted);
            var book = _mapper.Map<Book>(bookDb);

            return book;
        }
        public async Task<List<Book>> SearchAsync(FilterBook filterBook)
        {
            var booksDb = await _db.Books
                                   .Where(b => (filterBook.Title == null || b.Title.Contains(filterBook.Title))
                                             && (filterBook.Author == null || b.Author.Equals(filterBook.Author))
                                             && (filterBook.PublishedDate == null || b.PublishedDate == filterBook.PublishedDate))
                                   .ToListAsync();
            var books = _mapper.Map<List<Book>>(booksDb);

            return books;
        }
        public async Task<bool> UpdateAsync(Book b) 
        {
            var bookDb = await _db.Books.FindAsync(b.Id);
            if (bookDb == null)
            {
                return false;
            }
            _mapper.Map(b, bookDb);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteByIdAsync(int b) 
        { 
            var bookDb = await _db.Books.FindAsync(b);
            if (bookDb == null)
            {
                return false;
            }
            bookDb.IsDeleted = true;
            await _db.SaveChangesAsync();

            return true;
        }

     
    }
}
