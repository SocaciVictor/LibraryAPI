using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class AuthorRepository(LibraryDbContext _libraryDbContext, IMapper _mapper) : IAuthorRepository
    {
        public async Task<Author> AddAsync(Author author)
        {
            var authorDb = _mapper.Map<AuthorDb>(author);
            await _libraryDbContext.Authors.AddAsync(authorDb);
            await _libraryDbContext.SaveChangesAsync();

            var createdAuthor = _mapper.Map<Author>(authorDb);
            return createdAuthor;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var authorDb = await _libraryDbContext.Authors.FindAsync(id);
            if (authorDb == null)
            {
                return false;
            }
            authorDb.IsDeleted = true;
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            var authorsDb = await _libraryDbContext.Authors.ToListAsync();
           
            return _mapper.Map<List<Author>>(authorsDb);
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            var authorDb = await _libraryDbContext.Authors.FindAsync(id);
            var author = _mapper.Map<Author>(authorDb);

            return author;
        }

        public async Task<Author?> GetByNameAsync(string name, bool isDeleted)
        {
            var authorDb = await _libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Name == name && a.IsDeleted == isDeleted);

            return _mapper.Map<Author>(authorDb);
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var authorDb = await _libraryDbContext.Authors.FindAsync(author.Id);
            if (authorDb == null)
            {
                return false;
            }
            _mapper.Map(author, authorDb);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
