using System.ComponentModel.DataAnnotations;
using ETMS.Domain.Entities;

namespace ETMS.Service.DTOs;

public class BoardDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Board name is required")]
    public string Name { get; set; } = null!;

    public string ColorCode { get; set; } = "#000000";

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public DateTime? CreatedAt{ get; set; }
    public DateTime? UpdatedAt{ get; set; }

}
