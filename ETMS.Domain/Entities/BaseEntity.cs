using System.ComponentModel.DataAnnotations.Schema;
using ETMS.Domain.Entities;

namespace ETMS.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
 
}
