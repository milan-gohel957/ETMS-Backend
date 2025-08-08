namespace ETMS.Service.DTOs;

public class MoveTaskDto
{
    public int TaskIdToMove { get; set; }
    public int NewBoardId { get; set; }
    public int? PreviousTaskId { get; set; }
    public int? NextTaskId{ get; set; }
}
