using ETMS.Domain.Entities;
using ETMS.Service.DTOs;
using AutoMapper;

namespace ETMS.Service.Mappings;

public class BoardProfile : Profile
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
