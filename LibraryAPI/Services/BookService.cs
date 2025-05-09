using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using System.Net.WebSockets;

namespace LibraryAPI.Services
{
    public class BookService(IBookRepository _bookRepository, IAuthorRepository _authorRepository) : IBookService
    {
        public async Task<Result<Book>> CreateAsync(Book b)
        {
            var result = new Result<Book>();
            var book = await _bookRepository.GetByTitleAsync(b.Title);
            var author = await _authorRepository.GetByIdAsync(b.AuthorId);
            if (book != null)
            {
                result.Errors.Add($"Book with title: {book.Title} already exists");
            }
            if (author == null)
            {
                result.Errors.Add($"Author with id: {b.AuthorId} does not exist");
            }
            if (b.Quantity <= 0)
            {
                result.Errors.Add($"Book quantity must be greater than 0");
            }
            if (b.PublishedDate > DateTime.Now)
            {
                result.Errors.Add($"Book published date must be less than or equal to current date");
            }
            if (result.HasErrors)
            {
                return result;
            }
            var createdBook = await _bookRepository.AddAsync(b);
            result.Data = createdBook;
            return result;
        }

        public async Task<Result> DeleteByIdAsync(int id)
        {
            var result = new Result();
            var isDeleted = await _bookRepository.DeleteByIdAsync(id);
            if (isDeleted == false)
            {
                result.Errors.Add($"Book with id: {id} was not found!");
            }
           
            return result;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<List<Book>> SearchAsync(FilterBook filterBook)
        {
           return await _bookRepository.SearchAsync(filterBook);
        }

        public async Task<Result> UpdateAsync(Book b, string authorName)
        {
            var result = new Result<Book>();
            var existingBook = await _bookRepository.GetByIdAsync(b.Id);
            var idAuthor = await _authorRepository.GetByNameAsync(authorName);
            b.AuthorId = idAuthor.Id;
            if (existingBook == null)
            {
                result.Errors.Add($"Book with id: {b.Id} was not found!");
                return result;
            }
            if (existingBook.Title != b.Title)
            {
                var bookWithSameName = await _bookRepository.GetByTitleAsync(b.Title);
                if (bookWithSameName != null)
                {
                    result.Errors.Add($"Book with title: {b.Title} already exists");
                }
            }
            var author = await _authorRepository.GetByIdAsync(b.AuthorId);
            if (author == null)
            {
                result.Errors.Add($"Author with id: {b.AuthorId} does not exist");
            }
            if (b.Quantity <= 0)
            {
                result.Errors.Add($"Book quantity must be greater than 0");
            }
            if (b.PublishedDate > DateTime.Now)
            {
                result.Errors.Add($"Book published date must be less than or equal to current date");
            }
            if (result.HasErrors)
            {
                return result;
            }
            await _bookRepository.UpdateAsync(b);
            return result;
        }
    }
}
