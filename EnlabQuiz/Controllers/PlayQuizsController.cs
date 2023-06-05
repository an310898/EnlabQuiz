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
    public class PlayQuizsController : ControllerBase
    {
        private readonly EnlabQuizDbContext _context;

        public PlayQuizsController(EnlabQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/PlayQuizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayQuiz>>> GetPlayQuizzes()
        {
          if (_context.PlayQuizzes == null)
          {
              return NotFound();
          }
            return await _context.PlayQuizzes.ToListAsync();
        }

        // GET: api/PlayQuizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayQuiz>> GetPlayQuiz(int id)
        {
          if (_context.PlayQuizzes == null)
          {
              return NotFound();
          }
            var playQuiz = await _context.PlayQuizzes.FindAsync(id);

            if (playQuiz == null)
            {
                return NotFound();
            }

            return playQuiz;
        }

        // PUT: api/PlayQuizs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayQuiz(int id, PlayQuiz playQuiz)
        {
            if (id != playQuiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(playQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayQuizExists(id))
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

        // POST: api/PlayQuizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayQuiz>> PostPlayQuiz(PlayQuiz playQuiz)
        {
          if (_context.PlayQuizzes == null)
          {
              return Problem("Entity set 'EnlabQuizDbContext.PlayQuizzes'  is null.");
          }
            _context.PlayQuizzes.Add(playQuiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayQuiz", new { id = playQuiz.Id }, playQuiz);
        }

        // DELETE: api/PlayQuizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayQuiz(int id)
        {
            if (_context.PlayQuizzes == null)
            {
                return NotFound();
            }
            var playQuiz = await _context.PlayQuizzes.FindAsync(id);
            if (playQuiz == null)
            {
                return NotFound();
            }

            _context.PlayQuizzes.Remove(playQuiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayQuizExists(int id)
        {
            return (_context.PlayQuizzes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
