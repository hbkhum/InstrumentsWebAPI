using WebAPI.Models;

namespace WebAPI.DTO;

public class TestResultDto 
{
    public Guid TestResultId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sequence { get; set; }
    public string LowLimit { get; set; }
    public string HighLimit { get; set; }
    public string? Result { get; set; }
    public bool Status { get; set; }
    public Guid TestId { get; set; }
    public Guid GroupTestId { get; set; }
    public string GroupName { get; set; }
    public int GroupSequence { get; set; }

}