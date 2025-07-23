using System.ComponentModel.DataAnnotations;

namespace ETMS.Domain.Entities;

public class Board : BaseEntity
{
    [Required(ErrorMessage = "Board name is required")]
    public string Name { get; set; } = null!;

    public string ColorCode { get; set; } = "#000000";

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;
}