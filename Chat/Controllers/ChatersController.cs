using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chat.Data;
using Chat.Models;

namespace Chat.Controllers
{
    public class ChatersController : Controller
    {
        private readonly ChatContext _context;

        public ChatersController(ChatContext context)
        {
            _context = context;
        }

        // GET: Chaters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chater.ToListAsync());
        }

        // GET: Chaters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chater = await _context.Chater
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chater == null)
            {
                return NotFound();
            }

            return View(chater);
        }

        // GET: Chaters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chaters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,Message")] Chater chater)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chater);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chater);
        }

        // GET: Chaters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chater = await _context.Chater.FindAsync(id);
            if (chater == null)
            {
                return NotFound();
            }
            return View(chater);
        }

        // POST: Chaters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User,Message")] Chater chater)
        {
            if (id != chater.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chater);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChaterExists(chater.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chater);
        }

        // GET: Chaters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chater = await _context.Chater
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chater == null)
            {
                return NotFound();
            }

            return View(chater);
        }

        // POST: Chaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chater = await _context.Chater.FindAsync(id);
            _context.Chater.Remove(chater);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChaterExists(int id)
        {
            return _context.Chater.Any(e => e.Id == id);
        }
    }
}
