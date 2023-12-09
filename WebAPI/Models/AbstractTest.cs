namespace WebAPI.Models;

public abstract class AbstractTest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sequence { get; set; }
    public string LowLimit { get; set; }
    public string HighLimit { get; set; }
}