using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chat.Data;
using Chat.Models;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatersAPIController : ControllerBase
    {
        private readonly ChatContext _context;

        public ChatersAPIController(ChatContext context)
        {
            _context = context;
        }

        // GET: api/ChatersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chater>>> GetChater()
        {
            return await _context.Chater.ToListAsync();
        }

        // GET: api/ChatersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chater>> GetChater(int id)
        {
            var chater = await _context.Chater.FindAsync(id);

            if (chater == null)
            {
                return NotFound();
            }

            return chater;
        }

        // PUT: api/ChatersAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChater(int id, Chater chater)
        {
            if (id != chater.Id)
            {
                return BadRequest();
            }

            _context.Entry(chater).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChaterExists(id))
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

        // POST: api/ChatersAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Chater>> PostChater(Chater chater)
        {
            _context.Chater.Add(chater);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChater", new { id = chater.Id }, chater);
        }

        // DELETE: api/ChatersAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chater>> DeleteChater(int id)
        {
            var chater = await _context.Chater.FindAsync(id);
            if (chater == null)
            {
                return NotFound();
            }

            _context.Chater.Remove(chater);
            await _context.SaveChangesAsync();

            return chater;
        }

        private bool ChaterExists(int id)
        {
            return _context.Chater.Any(e => e.Id == id);
        }
    }
}
