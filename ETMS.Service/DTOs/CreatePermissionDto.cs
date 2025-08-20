using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class CreatePermissionDto
{
    [Required(ErrorMessage = "Permission name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Permission name must be between 3 and 100 characters.")]
    public string? Name { get; set; }
}