using AutoMapper;
using Hangman.Domain;

namespace Hangman.Web.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDTO, AppUser>().ReverseMap();
        }
    }
}
