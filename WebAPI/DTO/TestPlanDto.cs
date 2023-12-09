using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO;

public class TestPlanDto
{
    public Guid TestPlanId { get; set; }
    public string Name { set; get; }
    public List<GroupTestDto>? GroupTests { get; set; }
}