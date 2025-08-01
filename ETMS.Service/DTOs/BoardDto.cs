using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class BoardDto : BaseEntityDto
{
    [Required(ErrorMessage = "Board name is required")]
    public string Name { get; set; } = null!;

    public string ColorCode { get; set; } = "#000000";

    public string? Description { get; set; }

    public int ProjectId { get; set; }
        
    public IEnumerable<TaskDto>? Tasks{ get; set; }

}
