using System.ComponentModel.DataAnnotations;

namespace ETMS.Service.DTOs;

public class RoleDto : BaseEntityDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

}
