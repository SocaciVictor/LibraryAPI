using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class UserRepository(LibraryDbContext _libraryDbContext, IMapper _mapper) : IUserRepository
    {
        public async Task<User> AddAsync(User user)
        {
            var userDb = _mapper.Map<UserDb>(user);
            await _libraryDbContext.Users.AddAsync(userDb);
            await _libraryDbContext.SaveChangesAsync();

            var createdUser = _mapper.Map<User>(userDb);
            return createdUser;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var userDb = await _libraryDbContext.Users.FindAsync(id);
            if (userDb == null)
            {
                return false;
            }
            userDb.IsDeleted = true;
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var usersDb = await _libraryDbContext.Users.ToListAsync();

            return _mapper.Map<List<User>>(usersDb);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var userDb = await _libraryDbContext.Users.FindAsync(id);
            var user = _mapper.Map<User>(userDb);

            return user;
        }

        public async Task<User?> GetByNameAsync(string name, bool isDeleted = false)
        {
            var userDb = await _libraryDbContext.Users
                .FirstOrDefaultAsync(u => u.Name == name && u.IsDeleted == isDeleted);

            return _mapper.Map<User>(userDb);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var userDb = await _libraryDbContext.Users.FindAsync(user.Id);
            if (userDb == null)
            {
                return false;
            }
            _mapper.Map(user, userDb);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
