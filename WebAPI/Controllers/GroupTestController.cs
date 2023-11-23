using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
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
            var GroupTest = await _context.GroupTests.FirstOrDefaultAsync(t => t.GroupTestId == id);
            if (GroupTest == null)
            {
                return NotFound();
            }

            return Ok(GroupTest);
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
            var GroupTest = await _context.GroupTests.FirstOrDefaultAsync(t => t.GroupTestId == id);
            if (GroupTest == null)
            {
                return NotFound();
            }

            // Update properties
            GroupTest.Name = updatedGroupTest.Name;
            GroupTest.Description = updatedGroupTest.Description;
            GroupTest.Sequence = updatedGroupTest.Sequence;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var GroupTest = await _context.GroupTests.FirstOrDefaultAsync(t => t.GroupTestId == id);
            if (GroupTest == null)
            {
                return NotFound();
            }

            _context.GroupTests.Remove(GroupTest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}