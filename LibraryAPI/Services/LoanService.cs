using AutoMapper;
using LibraryAPI.Core.Helper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using LibraryAPI.Infrastructure.Repositories;
using LibraryAPI.Presentation.Dtos;

namespace LibraryAPI.Services
{
    public class LoanService(IBookRepository _bookRepository, ILoanRepository _loanRepository, IEmailSender _emailSender, IMapper _mapper) : ILoanService
    {
        public async Task<Result<Loan>> BorrowAsync(Loan loan, string notifyEmail)
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
            loan.LoanedAt = loan.LoanedAt;
            var created = await _loanRepository.AddAsync(loan);
            result.Data = created;
            var dto = _mapper.Map<LoanDto>(result.Data!);
            var body = $@"
                <h3>New Loan Created</h3>
                <p>Loan ID: {dto.Id}</p>
                <p>Book ID: {dto.BookId}</p>
                <p>User ID: {dto.UserId}</p>
                <p>Loaned At: {dto.LoanedAt:yyyy-MM-dd HH:mm}</p>";
            await _emailSender.SendAsync(notifyEmail, "Library: New Loan", body);
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var isDeleted = await _loanRepository.DeleteAsync(id);
            if (isDeleted == false)
            {
                result.Errors.Add($"Loan with id: {id} was not found!");
            }

            return result;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetLoans();
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _loanRepository.GetByIdAsync(loanId);
        }

        public async Task<Result> ReturnAsync(int loanId, string notifyEmail)
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
            var body = $@"
                <h3>Book Returned</h3>
                <p>Loan ID: {loanId}</p>
                <p>Returned At: {DateTime.UtcNow:yyyy-MM-dd HH:mm}</p>";
            await _emailSender.SendAsync(notifyEmail, "Library: Loan Returned", body);
            loan.ReturnedAt = DateTime.UtcNow;
            await _loanRepository.UpdateAsync(loan);

            return result;
        }

        public async Task<Result> UpdateAsync(Loan toUpdate)
        {
            var result = new Result();
            var loan = await _loanRepository.GetByIdAsync(toUpdate.Id);
            if (loan is null)
            {
                result.Errors.Add($"Loan with id: {toUpdate.Id} was not found!");
                return result;
            }
            if (toUpdate.LoanedAt > DateTime.UtcNow)
            {
                result.Errors.Add($"LoanedAt date cannot be in the future.");
                return result;
            }
            if (toUpdate.ReturnedAt != null && toUpdate.ReturnedAt < toUpdate.LoanedAt)
            {
                result.Errors.Add($"ReturnedAt date cannot be before LoanedAt date.");
                return result;
            }
            if (result.HasErrors)
            {
                return result;
            }
            loan.LoanedAt = toUpdate.LoanedAt;
            loan.ReturnedAt = toUpdate.ReturnedAt;
            loan.UserId = toUpdate.UserId;
            loan.BookId = toUpdate.BookId;
            loan.IsDeleted = toUpdate.IsDeleted;
            await _loanRepository.UpdateAsync(toUpdate);

            return result;
        }
    }
}
