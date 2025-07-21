using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Status : BaseEntity
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Status Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ColorCode { get; set; } = string.Empty;
}
