using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class CreateProjectDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Project Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int CreatedByUserId { get; set; }
    public bool IsAddDefaultBoards { get; set; } = false;
}
