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
    public class DailyAgendasController : ControllerBase
    {
        private readonly LifePlannerServerContext _context;

        public DailyAgendasController(LifePlannerServerContext context)
        {
            _context = context;
        }

        // GET: api/DailyAgendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyAgenda>>> GetDailyAgendas()
        {
            return await _context.DailyAgendas.ToListAsync();
        }

        // GET: api/DailyAgendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyAgenda>> GetDailyAgenda(int id)
        {
            var dailyAgenda = await _context.DailyAgendas.FindAsync(id);

            if (dailyAgenda == null)
            {
                return NotFound();
            }

            return dailyAgenda;
        }

        // PUT: api/DailyAgendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyAgenda(int id, DailyAgenda dailyAgenda)
        {
            if (id != dailyAgenda.Id)
            {
                return BadRequest();
            }

            _context.Entry(dailyAgenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyAgendaExists(id))
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

        // POST: api/DailyAgendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyAgenda>> PostDailyAgenda(DailyAgenda dailyAgenda)
        {
            _context.DailyAgendas.Add(dailyAgenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyAgenda", new { id = dailyAgenda.Id }, dailyAgenda);
        }

        // DELETE: api/DailyAgendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyAgenda(int id)
        {
            var dailyAgenda = await _context.DailyAgendas.FindAsync(id);
            if (dailyAgenda == null)
            {
                return NotFound();
            }

            _context.DailyAgendas.Remove(dailyAgenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyAgendaExists(int id)
        {
            return _context.DailyAgendas.Any(e => e.Id == id);
        }
    }
}
