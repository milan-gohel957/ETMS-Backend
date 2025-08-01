using ETMS.Domain.Entities;

namespace ETMS.Service.DTOs;

public class CreateBoardDto
{
    public string Name { get; set; } = null!;

    public string ColorCode { get; set; } = "#000000";

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public int? CreatedByUserId{ get; set; }
}   

