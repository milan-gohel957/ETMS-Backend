namespace ETMS.Service.DTOs;

public class ShiftTaskOrderRangeDto
{
    public int MinOrder { get; set; }

    public int MaxOrder { get; set; }

    public int BoardId { get; set; }
    
    public int ShiftAmount { get; set; }
}
