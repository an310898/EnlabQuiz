using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnlabQuiz.Models;

namespace EnlabQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryPlaysController : ControllerBase
    {
        private readonly EnlabQuizDbContext _context;

        public HistoryPlaysController(EnlabQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/HistoryPlays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryPlay>>> GetHistoryPlays()
        {
          if (_context.HistoryPlays == null)
          {
              return NotFound();
          }
            return await _context.HistoryPlays.ToListAsync();
        }

        // GET: api/HistoryPlays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryPlay>> GetHistoryPlay(int id)
        {
          if (_context.HistoryPlays == null)
          {
              return NotFound();
          }
            var historyPlay = await _context.HistoryPlays.FindAsync(id);

            if (historyPlay == null)
            {
                return NotFound();
            }

            return historyPlay;
        }

        // PUT: api/HistoryPlays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryPlay(int id, HistoryPlay historyPlay)
        {
            if (id != historyPlay.Id)
            {
                return BadRequest();
            }

            _context.Entry(historyPlay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryPlayExists(id))
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

        // POST: api/HistoryPlays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryPlay>> PostHistoryPlay(HistoryPlay historyPlay)
        {
          if (_context.HistoryPlays == null)
          {
              return Problem("Entity set 'EnlabQuizDbContext.HistoryPlays'  is null.");
          }
            _context.HistoryPlays.Add(historyPlay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryPlay", new { id = historyPlay.Id }, historyPlay);
        }

        // DELETE: api/HistoryPlays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryPlay(int id)
        {
            if (_context.HistoryPlays == null)
            {
                return NotFound();
            }
            var historyPlay = await _context.HistoryPlays.FindAsync(id);
            if (historyPlay == null)
            {
                return NotFound();
            }

            _context.HistoryPlays.Remove(historyPlay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryPlayExists(int id)
        {
            return (_context.HistoryPlays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
