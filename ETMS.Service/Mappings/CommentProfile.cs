using AutoMapper;
using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using ETMS.Service.Mappings.Interfaces;

namespace ETMS.Service.Mappings;

public class CommentProfile : Profile, IAutoMapperProfile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>();

        CreateMap<CreateCommentDto, Comment>();

        CreateMap<UpdateCommentDto, Comment>();

        CreateMap<CommentMention, CommentMentionDto>()
         .ForMember(dest => dest.CurrentUserName,
                    opt => opt.MapFrom(src => src.User!.UserName)) // take username from navigation property
         .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId));
    }
}
