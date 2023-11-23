using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class TestResult
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TestResultId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sequence { get; set; }
    public string LowLimit { get; set; }
    public string Result { get; set; }
    public string HighLimit { get; set; }
    public bool Status { get; set; }
    public Guid TestId { get; set; }
    public Test Test { get; set; }

}