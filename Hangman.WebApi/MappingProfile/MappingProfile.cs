using AutoMapper;
using Hangman.Domain;

namespace Hangman.Web.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDTO, AppUser>().ForMember(au => au.FirstName, map => map.MapFrom(rdto => rdto.FirstName)).ForMember(au => au.LastName, map => map.MapFrom(rdto => rdto.LastName)).ForMember(au => au.PhoneNumber, map => map.MapFrom(rdto => rdto.PhoneNumber)).ForMember(au => au.Address, map => map.MapFrom(rdto => rdto.Address)).ForMember(au => au.Email, map => map.MapFrom(rdto => rdto.Email)).ForMember(au => au.UserName, map => map.MapFrom(rdto => rdto.UserName)).ForMember(au => au.ImageUrl, map => map.MapFrom(rdto => rdto.ImageUrl)).ReverseMap();
            CreateMap<GameDataDTO, GameData>().ReverseMap();
        }
    }
}
