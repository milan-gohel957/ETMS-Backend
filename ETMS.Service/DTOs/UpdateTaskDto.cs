using System.ComponentModel.DataAnnotations;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.DTOs;

public class UpdateTaskDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Task Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int StatusId { get; set; } = (int)StatusEnum.Pending;
    public int BoardId { get; set; }
}
