using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto?> BorrowAsync(LoanRequestDto dto);
        Task<bool> ReturnAsync(int loanId);
        Task<LoanDto?> GetByIdAsync(int id);
    }
}
