using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ApplicationDb _context;

    public TestController(ILogger<TestController> logger, ApplicationDb context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Tests.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
        if (test == null)
        {
            return NotFound();
        }

        return Ok(test);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Test newTest)
    {
        newTest.TestId = Guid.NewGuid();
        _context.Tests.Add(newTest);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = newTest.TestId }, newTest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Test updatedTest)
    {
        var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
        if (test == null)
        {
            return NotFound();
        }

        // Update properties
        test.Name = updatedTest.Name;
        test.Description = updatedTest.Description;
        test.Sequence = updatedTest.Sequence;
        test.HighLimit = updatedTest.HighLimit;
        test.LowLimit = updatedTest.LowLimit;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
        if (test == null)
        {
            return NotFound();
        }

        _context.Tests.Remove(test);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}