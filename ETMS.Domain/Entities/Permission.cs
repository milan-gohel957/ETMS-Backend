using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Permission : BaseEntity
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

}   