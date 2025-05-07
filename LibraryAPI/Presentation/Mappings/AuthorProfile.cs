using AutoMapper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using LibraryAPI.Presentation.Dtos;

namespace LibraryAPI.Presentation.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDb, Author>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDb>();
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
        }
    }
}
