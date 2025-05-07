using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> AddAsync(Book book);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByTitleAsync(string title, bool isDeleted = false);
        Task<List<Book>> SearchAsync(FilterBook filterBook);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteByIdAsync(int id);


    }
}
