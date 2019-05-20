using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly ProblemContext _context;
        public ProblemController(ProblemContext context)
        {
            _context = context;
            if (_context.ProblemItems.Count() == 0)
            {
                _context.ProblemItems.Add(new ProblemItem
                {
                    Name = "Item1"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet] public async Task<ActionResult<IEnumerable<ProblemItem>>> GetProblemItems()
        {
            return await _context.ProblemItems.ToListAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemItem>> GetProblemItem(long id)
        {
            var ProblemItem = await _context.ProblemItems.FindAsync(id);

            if (ProblemItem == null) { return NotFound(); }

            return ProblemItem;
        }        

        [HttpPost]
        public async Task<ActionResult<ProblemItem>> PostProblemItem(ProblemItem item)
        {
            _context.ProblemItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProblemItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblemItem(long id, ProblemItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblemItem(long id)
        {
            var ProblemItem = await _context.ProblemItems.FindAsync(id);

            if (ProblemItem == null)
            {
                return NotFound();
            }

            _context.ProblemItems.Remove(ProblemItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    
}