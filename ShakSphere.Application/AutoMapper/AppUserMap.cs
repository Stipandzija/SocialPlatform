using AutoMapper;
using ShakSphere.Application.Contracts.UserProfile.Request;
using ShakSphere.Application.Contracts.UserProfile.Response;
using ShakSphere.Application.UseCases.AppUserProfile.Commands;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.AutoMapper
{
    internal class AppUserMap : Profile
    {
        public AppUserMap()
        {
            CreateMap<AppUserCreateRequestDTO, CreateUserCommand>();
            CreateMap<ApplicationUser, AppUserResponseDTO>();
            CreateMap<BasicInfo, BasicInfoResponseDTO>();
            CreateMap<AppUserUpdateRequestDTO, UpdateUserCommand>();
        }

    }
}
