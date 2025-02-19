using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Contracts.Posts.Request;
using ShakSphere.Application.Contracts.Posts.Response;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using System.Security.Cryptography.X509Certificates;

namespace ShakSphere.Application.AutoMapper
{
    public class PostMap:Profile
    {
        public PostMap()
        {
            CreateMap<Post, PostResponseDTO>()
                      .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId.ToString()))
                      .ForMember(dest => dest.InteractionCount, opt => opt.MapFrom(src => src.PostInteraction.Count))
                      .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
            CreateMap<PostComment, PostCommentResponseDTO>()
                      .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                      .ForMember(dest => dest.UserProfileID, opt => opt.MapFrom(src => src.AppUserId));
            CreateMap<Post, PostCreateDTO>();
            CreateMap<Post, PostCreateResponseDTO>()
                .ForMember(dest => dest.AppuserId, opt => opt.MapFrom(src=>src.AppUserId.ToString()));
        }
    }
}
