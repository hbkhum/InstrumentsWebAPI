using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Test
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TestId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sequence { get; set; }
    public string LowLimit { get; set; }
    public string Result { get; set; }
    public string HighLimit { get; set; }
    public bool Status { get; set; }

    public Guid GroupTestId { get; set; }
    public GroupTest GroupTest { get; set; }
}