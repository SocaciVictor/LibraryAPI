using AutoMapper;
using LibraryAPI.Core.Models;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Presentation.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDb>();
            CreateMap<UserDb, User>();
        }
    }
}
