using AutoMapper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;
using LibraryAPI.Presentation.Dtos;

namespace LibraryAPI.Presentation.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserDb>();
            CreateMap<UserDb, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
