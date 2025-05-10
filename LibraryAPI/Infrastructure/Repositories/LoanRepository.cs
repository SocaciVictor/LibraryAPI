using AutoMapper;
using LibraryAPI.Core.Interfaces;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class LoanRepository(LibraryDbContext _db, IMapper _mapper) : ILoanRepository
    {
        public async Task<Loan> AddAsync(Loan book)
        {
            var loanDb = _mapper.Map<LoanDb>(book);
            await _db.Loans.AddAsync(loanDb);
            await _db.SaveChangesAsync();
            var createdLoan = _mapper.Map<Loan>(loanDb);

            return createdLoan;
        }

        public async Task<int> CountActiveByBookIdAsync(int bookId)
        {
            return await _db.Loans
                .CountAsync(l => l.BookId == bookId && l.ReturnedAt == null && !l.IsDeleted);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var loanDb = await _db.Loans.FindAsync(id);
            if(loanDb == null)
            {
                return false;
            }
            loanDb.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            var loanDb = await _db.Loans.FindAsync(id);
            var loan = _mapper.Map<Loan>(loanDb);

            return loan;
        }

        public async Task<List<Loan>?> GetLoans()
        {
            var loansDb = await _db.Loans.ToListAsync();

            return _mapper.Map<List<Loan>>(loansDb);
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            var loanDb = await _db.Loans.FindAsync(loan.Id);
            if (loanDb == null)
            {
                return false;
            }
            _mapper.Map(loan, loanDb);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
