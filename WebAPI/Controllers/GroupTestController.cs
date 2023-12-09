using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupTestController : ControllerBase
    {
        private readonly ILogger<GroupTestController> _logger;
        private readonly ApplicationDb _context;

        public GroupTestController(ILogger<GroupTestController> logger, ApplicationDb context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.GroupTests.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var groupTest = await _context.GroupTests.Include(t => t.Tests)
                .FirstOrDefaultAsync(t => t.GroupTestId == id);

            if (groupTest == null)
            {
                return NotFound();
            }

            var groupTestDto = new GroupTestDto
            {
                GroupTestId = groupTest.GroupTestId,
                Name = groupTest.Name,
                Description = groupTest.Description,
                Sequence = groupTest.Sequence,
                Tests = groupTest?.Tests.Select(test => new TestDto
                {
                    TestId = test.TestId,
                    Name = test.Name,
                    Description = test.Description,
                    Sequence = test.Sequence,
                    LowLimit = test.LowLimit,
                    HighLimit = test.HighLimit
                }).ToList()
            };

            return Ok(groupTestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupTest newGroupTest)
        {
            newGroupTest.GroupTestId = Guid.NewGuid();
            _context.GroupTests.Add(newGroupTest);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newGroupTest.GroupTestId }, newGroupTest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] GroupTest updatedGroupTest)
        {
            var groupTest = await _context.GroupTests.FirstOrDefaultAsync(t => t.GroupTestId == id);
            if (groupTest == null)
            {
                return NotFound();
            }

            // Update properties
            groupTest.Name = updatedGroupTest.Name;
            groupTest.Description = updatedGroupTest.Description;
            groupTest.Sequence = updatedGroupTest.Sequence;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var groupTest = await _context.GroupTests.FirstOrDefaultAsync(t => t.GroupTestId == id);
            if (groupTest == null)
            {
                return NotFound();
            }

            _context.GroupTests.Remove(groupTest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}