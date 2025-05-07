using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Services
{
    public class LoanService(IBookRepository _bookRepository, ILoanRepository _loanRepository) : ILoanService
    {
        public async Task<Result<Loan>> BorrowAsync(Loan loan)
        {
            var result = new Result<Loan>();

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book is null)
            {
                result.Errors.Add("Book not found.");
                return result;
            }
            var activeLoans = await _loanRepository.CountActiveByBookIdAsync(loan.BookId);
            if (book.Quantity <= activeLoans)
            {
                result.Errors.Add("No copies available for borrowing.");
                return result;
            }
            loan.LoanedAt = DateTime.UtcNow;
            var created = await _loanRepository.AddAsync(loan);
            result.Data = created;
            return result;
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _loanRepository.GetByIdAsync(loanId);
        }

        public async Task<Result> ReturnAsync(int loanId)
        {
            var result = new Result();

            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan is null)
            {
                result.Errors.Add("Loan not found.");
                return result;
            }

            if (loan.ReturnedAt != null)
            {
                result.Errors.Add("Loan has already been returned.");
                return result;
            }

            loan.ReturnedAt = DateTime.UtcNow;
            await _loanRepository.UpdateAsync(loan);

            return result;
        }
    }
}
