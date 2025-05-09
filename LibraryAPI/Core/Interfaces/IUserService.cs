using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> CreateAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByNameAsync(string name);
        Task<Result> UpdateAsync(User user);
        Task<Result> DeleteByIdAsync(int id);

    }
}
