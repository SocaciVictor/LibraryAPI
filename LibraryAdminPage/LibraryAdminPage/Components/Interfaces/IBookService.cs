using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto?> CreateAsync(CreateBookDto dto);
        Task<bool> UpdateAsync(int id, UpdateBookDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
