using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ETMS.Domain.Entities;

namespace ETMS.Domain.Entities;

public class BaseEntity
{
    [Required]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsDeleted { get; set; }
 
}
