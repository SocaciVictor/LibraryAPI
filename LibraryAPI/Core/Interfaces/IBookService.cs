using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface IBookService
    {
        Task<Result<Book>> CreateAsync(Book b);
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>> SearchAsync(FilterBook filterBook);
        Task<Result> UpdateAsync(Book b, string authorName);
        Task<Result> DeleteByIdAsync(int id);
    }
}
