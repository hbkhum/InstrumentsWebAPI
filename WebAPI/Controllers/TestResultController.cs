using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestResultController : ControllerBase
{
    private readonly ILogger<TestResultController> _logger;
    private readonly ApplicationDb _context;

    public TestResultController(ILogger<TestResultController> logger, ApplicationDb context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.TestResults.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var testResult = await _context.TestResults
            .Include(c => c.Test)
            .ThenInclude(c => c.GroupTest).FirstOrDefaultAsync(t => t.TestResultId == id);

        var testResultDto = new TestResultDto
        {
            TestResultId = testResult.TestResultId,
            Name = testResult.Name,
            Description = testResult.Description,
            Sequence = testResult.Sequence,
            LowLimit = testResult.LowLimit,
            HighLimit = testResult.HighLimit,
            Result = testResult.Result,
            Status = testResult.Status,
            TestId = testResult.TestId,
            GroupTestId = testResult.Test?.GroupTestId ?? Guid.Empty,
            GroupName = testResult.Test?.GroupTest?.Name,
            GroupSequence = testResult.Test?.GroupTest?.Sequence ?? 0 
        };

        return Ok(testResult);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TestResult newTestResult)
    {
        newTestResult.TestResultId = Guid.NewGuid();
        _context.TestResults.Add(newTestResult);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newTestResult.TestResultId }, newTestResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] TestResult updatedTestResult)
    {
        var testResult = await _context.TestResults.FirstOrDefaultAsync(t => t.TestResultId == id);
        if (testResult == null)
        {
            return NotFound();
        }

        // Update properties
        testResult.Name = updatedTestResult.Name;
        testResult.Description = updatedTestResult.Description;
        testResult.Sequence = updatedTestResult.Sequence;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var testResult = await _context.TestResults.FirstOrDefaultAsync(t => t.TestResultId == id);
        if (testResult == null)
        {
            return NotFound();
        }

        _context.TestResults.Remove(testResult);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}