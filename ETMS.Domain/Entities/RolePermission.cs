using System.ComponentModel.DataAnnotations.Schema;

namespace ETMS.Domain.Entities;

public class RolePermission : BaseEntity
{
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    [ForeignKey("Permission")]
    public int PermissionId { get; set; }
    public virtual Role Role { get; set; } = new Role();
    public virtual Permission Permission { get; set; } = new();


}
