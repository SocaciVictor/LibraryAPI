using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Core.Interfaces
{
    public interface ILoanService
    {
        Task<Result<Loan>> BorrowAsync(Loan loan);
        Task<Loan?> GetByIdAsync(int loanId);
        Task<Result>ReturnAsync(int loanId);
    }
}
