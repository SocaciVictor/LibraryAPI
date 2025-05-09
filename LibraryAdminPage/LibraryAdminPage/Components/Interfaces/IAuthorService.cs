using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto?> CreateAsync(CreateAuthorDto dto);
        Task<bool> UpdateAsync(int id, UpdateAuthorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
