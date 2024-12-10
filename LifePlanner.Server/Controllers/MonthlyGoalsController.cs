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
    public class MonthlyGoalsController : ControllerBase
    {
        private readonly LifePlannerServerContext _context;

        public MonthlyGoalsController(LifePlannerServerContext context)
        {
            _context = context;
        }

        // GET: api/MonthlyGoals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonthlyGoal>>> GetMonthlyGoals()
        {
            return await _context.MonthlyGoals.ToListAsync();
        }

        // GET: api/MonthlyGoals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonthlyGoal>> GetMonthlyGoal(int id)
        {
            var monthlyGoal = await _context.MonthlyGoals.FindAsync(id);

            if (monthlyGoal == null)
            {
                return NotFound();
            }

            return monthlyGoal;
        }

        // PUT: api/MonthlyGoals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonthlyGoal(int id, MonthlyGoal monthlyGoal)
        {
            if (id != monthlyGoal.Id)
            {
                return BadRequest();
            }

            _context.Entry(monthlyGoal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonthlyGoalExists(id))
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

        // POST: api/MonthlyGoals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonthlyGoal>> PostMonthlyGoal(MonthlyGoal monthlyGoal)
        {
            _context.MonthlyGoals.Add(monthlyGoal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonthlyGoal", new { id = monthlyGoal.Id }, monthlyGoal);
        }

        // DELETE: api/MonthlyGoals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonthlyGoal(int id)
        {
            var monthlyGoal = await _context.MonthlyGoals.FindAsync(id);
            if (monthlyGoal == null)
            {
                return NotFound();
            }

            _context.MonthlyGoals.Remove(monthlyGoal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonthlyGoalExists(int id)
        {
            return _context.MonthlyGoals.Any(e => e.Id == id);
        }
    }
}
