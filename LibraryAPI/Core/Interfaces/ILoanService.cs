using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface ILoanService
    {
        Task<Result<Loan>> BorrowAsync(Loan loan, string notifyEmail);
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int loanId);
        Task<Result>ReturnAsync(int loanId, string notifyEmail);
    }
}
