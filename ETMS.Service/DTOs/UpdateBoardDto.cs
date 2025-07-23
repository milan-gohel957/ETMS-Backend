namespace ETMS.Service.DTOs;

public class UpdateBoardDto
{
    public string Name { get; set; } = null!;

    public string ColorCode { get; set; } = "#000000";

    public string? Description { get; set; }
}
