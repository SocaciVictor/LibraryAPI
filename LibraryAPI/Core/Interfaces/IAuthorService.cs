using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface IAuthorService
    {
        Task<Result<Author>> CreateAsync(Author author);
        Task<Author?> GetAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<Result> UpdateAsync(Author author);
        Task<Result> DeleteAsync(int id);
    }
}
