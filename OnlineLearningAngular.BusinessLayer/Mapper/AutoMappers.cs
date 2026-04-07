using AutoMapper;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.BusinessLayer.Mapper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<ApplicationUser, AuthResponse>()
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)))
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<RegisterRequest, ApplicationUser>();

            CreateMap<UpdateUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
