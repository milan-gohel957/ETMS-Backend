using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using AutoMapper;
using ETMS.Service.Mappings.Interfaces;

namespace ETMS.Service.Mappings;

public class BoardProfile : Profile, IAutoMapperProfile
{
    public BoardProfile()
    {
        
        CreateMap<Board, BoardDto>();

        CreateMap<CreateBoardDto, Board>().ReverseMap();
        CreateMap<CreateBoardDto, Board>();
        CreateMap<UpdateBoardDto, Board>().ReverseMap();
        CreateMap<UpdateBoardDto, Board>();
    }
}
