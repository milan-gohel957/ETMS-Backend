using System.ComponentModel.DataAnnotations;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Domain.Entities;

public class Milestone : BaseEntity
{

    [Required(AllowEmptyStrings = false, ErrorMessage = "Milestone Name is Required.")]
    [RegularExpression(@"\S+", ErrorMessage = "Milestone Name cannot be empty or whitespace.")]
    public string Name { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; }

    public int StatusId { get; set; } = (int)StatusEnum.Pending;

    public Status? Status { get; set; } 
    

}