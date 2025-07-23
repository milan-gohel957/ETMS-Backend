using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ETMS.Domain.Entities;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Project Name is Required.")]
    [RegularExpression(@"\S+", ErrorMessage = "Project Name cannot be empty or whitespace.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [ForeignKey("Status")]
    public int StatusId { get; set; } = (int)StatusEnum.Pending;
    public Status Status { get; set; } = null!;
}
