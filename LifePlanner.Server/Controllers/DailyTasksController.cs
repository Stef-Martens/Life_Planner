using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifePlanner.Server.Data;
using LifePlanner.Server.Models;

namespace LifePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyTasksController : ControllerBase
    {
        private readonly LifePlannerServerContext _context;

        public DailyTasksController(LifePlannerServerContext context)
        {
            _context = context;
        }

        // GET: api/DailyTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyTask>>> GetDailyTasks()
        {
            return await _context.DailyTasks.ToListAsync();
        }

        // GET: api/DailyTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyTask>> GetDailyTask(int id)
        {
            var dailyTask = await _context.DailyTasks.FindAsync(id);

            if (dailyTask == null)
            {
                return NotFound();
            }

            return dailyTask;
        }

        // PUT: api/DailyTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyTask(int id, DailyTask dailyTask)
        {
            if (id != dailyTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(dailyTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DailyTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyTask>> PostDailyTask(DailyTask dailyTask)
        {
            _context.DailyTasks.Add(dailyTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyTask", new { id = dailyTask.Id }, dailyTask);
        }

        // DELETE: api/DailyTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyTask(int id)
        {
            var dailyTask = await _context.DailyTasks.FindAsync(id);
            if (dailyTask == null)
            {
                return NotFound();
            }

            _context.DailyTasks.Remove(dailyTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyTaskExists(int id)
        {
            return _context.DailyTasks.Any(e => e.Id == id);
        }
    }
}
