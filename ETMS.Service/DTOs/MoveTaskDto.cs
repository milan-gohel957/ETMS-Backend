namespace ETMS.Service.DTOs;

public class MoveTaskDto
{
    public int NewBoardId { get; set; }
    public int? PreviousTaskId { get; set; }
    public int? NextTaskId{ get; set; }
}
