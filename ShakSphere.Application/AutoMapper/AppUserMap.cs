using AutoMapper;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Contracts.Posts.Request;
using ShakSphere.Application.Contracts.Posts.Response;
using ShakSphere.Application.Contracts.UserProfile.Request;
using ShakSphere.Application.Contracts.UserProfile.Response;
using ShakSphere.Application.UseCases.AppUserProfile.Commands;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
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
            CreateMap<Post, PostResponseDTO>()
                       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                       .ForMember(dest => dest.InteractionCount, opt => opt.MapFrom(src => src.PostInteraction.Count))
                       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
            CreateMap<PostComment, PostCommentResponseDTO>();
            CreateMap<Post, PostCreateDTO>();
        }

    }
}
