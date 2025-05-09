using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Repositories;
using System.Linq;

namespace LibraryAPI.Services
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public async Task<Result<User>> CreateAsync(User user)
        {
            var result = new Result<User>();
            var userWithSameName = await _userRepository.GetByNameAsync(user.Name);
            if (userWithSameName != null)
            {
                if (userWithSameName.IsDeleted)
                {
                    user.IsDeleted = false;
                    await _userRepository.UpdateAsync(userWithSameName);
                    var created = await _userRepository.GetByIdAsync(userWithSameName.Id);
                    result.Data = created;
                    return result;
                }
                else
                {
                    result.Errors.Add($"User with name: {user.Name} already exists");
                }
            }
            if (result.HasErrors)
            {
                return result;
            }
            var createdUser = await _userRepository.AddAsync(user);
            result.Data = createdUser;
            return result;

        }

        public async Task<Result> DeleteByIdAsync(int id)
        {
            var result = new Result();
            var isDeleted = await _userRepository.DeleteByIdAsync(id);
            if (isDeleted == false)
            {
                result.Errors.Add($"User with id: {id} was not found!");
            }

            return result;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByNameAsync(string name)
        { 
            return await _userRepository.GetByNameAsync(name);
        }

        public async Task<Result> UpdateAsync(User user)
        {
            var result = new Result<User>();
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                result.Errors.Add($"User with id: {user.Id} was not found!");
                return result;
            }
            if (existingUser.Name != user.Name)
            {
                var userWithSameName = await _userRepository.GetByNameAsync(user.Name);
                if (userWithSameName != null)
                {
                    result.Errors.Add($"User with name: {user.Name} already exists");
                    return result;
                }
            }
            if (result.HasErrors)
            {
                return result;
            }
            await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
