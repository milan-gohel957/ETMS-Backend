using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class CreateProjectDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Project Name is Required.")]
    [RegularExpression(@"\S+", ErrorMessage = "Project Name cannot be empty or whitespace.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CreatedByUserId{ get; set; }
}
