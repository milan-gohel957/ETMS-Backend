using System.ComponentModel.DataAnnotations;

namespace ETMS.Domain.Entities;

public class CommentMention :BaseEntity
{
    [Required]
    public int CommentId { get; set; }
    public Comment? Comment{ get; set; }
    [Required]
    public int UserId{ get; set; }
    public User? User{ get; set; }
}

