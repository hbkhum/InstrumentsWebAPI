using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class TestPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TestPlanId { get; set; }
    public string Name { set; get; }
    public IEnumerable<GroupTest>? GroupTests { get; set; }

}