using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class PermissionDto
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
