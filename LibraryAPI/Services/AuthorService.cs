using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using Microsoft.VisualBasic;

namespace LibraryAPI.Services
{
    public class AuthorService(IAuthorRepository _authorRepository) : IAuthorService
    {

        public async Task<Result<Author>> CreateAsync(Author author)
        {
            var result = new Result<Author>();
            var authorWithSameName = await _authorRepository.GetByNameAsync(author.Name);
            if (authorWithSameName != null)
            {
                if (authorWithSameName.IsDeleted)
                {
                    author.IsDeleted = false;
                    await _authorRepository.UpdateAsync(authorWithSameName);
                    var created = await _authorRepository.GetByIdAsync(authorWithSameName.Id);
                    result.Data = created;
                    return result;
                }
                else
                {
                    result.Errors.Add($"Author with name: {author.Name} already exists");
                }
            }
            if(result.HasErrors)
            {
                return result;
            }
            var createdAuthor = await _authorRepository.AddAsync(author);
            result.Data = createdAuthor;
            return result;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var result = new Result();
            var isDeleted = await _authorRepository.DeleteByIdAsync(id);
            if (isDeleted == false)
            {
                result.Errors.Add($"Author with id: {id} was not found!");
            }

            return result;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Result> UpdateAsync(Author author)
        {
            var result = new Result<Author>();
            var existingAuthor = await _authorRepository.GetByIdAsync(author.Id);
            if (existingAuthor == null)
            {
                result.Errors.Add($"Author with id: {author.Id} was not found!");
                return result;
            }
            if (existingAuthor.Name != author.Name) 
            {
                var authorWithSameName = await _authorRepository.GetByNameAsync(author.Name);
                if (authorWithSameName != null)
                {
                    result.Errors.Add($"Author with name: {author.Name} already exists");
                }
            }
            if (result.HasErrors)
            {
                return result;
            }
            await _authorRepository.UpdateAsync(author);
            return result;
        }
    }
}
