namespace WebAPI.DTO;

public class TestDto
{
    public Guid TestId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sequence { get; set; }
    public string LowLimit { get; set; }
    public string HighLimit { get; set; }
}