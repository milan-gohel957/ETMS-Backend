using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class Role : BaseEntity
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<UserProjectRole>? UserProjectRoles { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
}
