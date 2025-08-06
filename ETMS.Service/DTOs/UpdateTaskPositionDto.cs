namespace ETMS.Service.DTOs;

public class UpdateTaskPositionDto
{
    public int TaskId { get; set; }
    public int NewBoardId { get; set; }
    public int NewPosition { get; set; }
}
