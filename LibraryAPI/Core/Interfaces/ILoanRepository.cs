using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> AddAsync(Loan book);
        Task<Loan?> GetByIdAsync(int id);
        Task<List<Loan>?> GetLoans();
        Task<bool> UpdateAsync(Loan book);
        Task<bool> DeleteAsync(Loan id);
        Task<int> CountActiveByBookIdAsync(int bookId);
    }
}
