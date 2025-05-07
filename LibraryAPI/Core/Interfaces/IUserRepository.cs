using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteByIdAsync(int id);
        Task<User?> GetByNameAsync(string name, bool isDeleted = false);
    }
}
