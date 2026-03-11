using AutoMapper;
using Tasks_Management_System.Application.DTOs.Auth;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            //register
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            //login
            CreateMap<LoginDto, ApplicationUser>();

            //logout
            CreateMap<ApplicationUser, AuthResponseDto>();
        }
    }
}