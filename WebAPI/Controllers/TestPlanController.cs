using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestPlanController : ControllerBase
{
    private readonly ILogger<TestPlanController> _logger;
    private readonly ApplicationDb _context;

    public TestPlanController(ILogger<TestPlanController> logger, ApplicationDb context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.TestPlans.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var testPlan = await _context.TestPlans
            .Include(t => t.GroupTests)
            .FirstOrDefaultAsync(t => t.TestPlanId == id);

        if (testPlan == null)
        {
            return NotFound();
        }

        var testPlanDto = new TestPlanDto
        {
            TestPlanId = testPlan.TestPlanId,
            Name = testPlan.Name,
            GroupTests = testPlan?.GroupTests.Select(group => new GroupTestDto
            {
                GroupTestId = group.GroupTestId,
                Name = group.Name,
                Description = group.Description,
                Sequence = group.Sequence
            }).ToList()
        };

        return Ok(testPlanDto);
    }

    [HttpGet("{id}/TestPlan")]
    public async Task<IActionResult> TestPlan(Guid id)
    {
        var testPlan = await _context.TestPlans
            .Include(tp => tp.GroupTests)
            .ThenInclude(gt => gt.Tests)
            .FirstOrDefaultAsync(tp => tp.TestPlanId == id);

        if (testPlan == null)
        {
            return NotFound();
        }

        var testResultDtos = testPlan.GroupTests
            .SelectMany(gt => gt.Tests)
            .Select(tr => new TestResultDto
            {
                Name = tr.Name,
                Description = tr.Description,
                Sequence = tr.Sequence,
                LowLimit = tr.LowLimit,
                HighLimit = tr.HighLimit,
                TestId = tr.TestId,
                GroupTestId = tr.GroupTest.GroupTestId,
                GroupName = tr.GroupTest.Name,
                GroupSequence = tr.GroupTest.Sequence
            }).OrderBy(tr => tr.GroupSequence)
            .ThenBy(tr => tr.Sequence)
            .ToList();



        return Ok(testResultDtos);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TestPlan newTestPlan)
    {
        newTestPlan.TestPlanId = Guid.NewGuid();
        _context.TestPlans.Add(newTestPlan);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newTestPlan.TestPlanId }, newTestPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] TestPlan updatedTestPlan)
    {
        var testPlan = await _context.TestPlans.FirstOrDefaultAsync(t => t.TestPlanId == id);
        if (testPlan == null)
        {
            return NotFound();
        }

        // Update properties
        testPlan.Name = updatedTestPlan.Name;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var testPlan = await _context.TestPlans.FirstOrDefaultAsync(t => t.TestPlanId == id);
        if (testPlan == null)
        {
            return NotFound();
        }

        _context.TestPlans.Remove(testPlan);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}