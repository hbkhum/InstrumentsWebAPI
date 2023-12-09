using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class TestResult : AbstractTest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TestResultId { get; set; }
    public string? Result { get; set; }
    public bool Status { get; set; }
    public Guid TestId { get; set; }
    public Test? Test { get; set; }

}