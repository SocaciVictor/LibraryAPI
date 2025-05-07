using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteByIdAsync(int id);
        Task<Author?> GetByNameAsync(string name, bool isDeleted = false);
    }
}
