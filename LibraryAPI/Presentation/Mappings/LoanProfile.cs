using AutoMapper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using LibraryAPI.Presentation.Dtos;

namespace LibraryAPI.Presentation.Mappings
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<LoanDto, Loan>();
            CreateMap<LoanRequestDto, Loan>();
            CreateMap<Loan, LoanRequestDto>();
            CreateMap<Loan, LoanDto>();
            CreateMap<Loan, LoanDb>();
            CreateMap<LoanDb, Loan>();
        }
    }
}
