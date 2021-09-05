using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using joke.Data;
using joke.Models;
using Microsoft.AspNetCore.Authorization;

namespace joke.Controllers
{
    public class jokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public jokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: jokes
        public async Task<IActionResult> Index()
        {
            return View(await _context.jokes.ToListAsync());
        }
        // GET: jokes/ShowSerachforms
        public async Task<IActionResult> ShowSearchforms()
        {
            return View();
        }
        // GET: jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.jokes.Where(j => j.jokequestion.Contains(SearchPhrase)).ToListAsync());
        }
        // GET: jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // GET: jokes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,jokequestion,jokeanswer")] jokes jokes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // GET: jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes.FindAsync(id);
            if (jokes == null)
            {
                return NotFound();
            }
            return View(jokes);
        }

        // POST: jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,jokequestion,jokeanswer")] jokes jokes)
        {
            if (id != jokes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jokesExists(jokes.ID))
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
            return View(jokes);
        }

        // GET: jokes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // POST: jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jokes = await _context.jokes.FindAsync(id);
            _context.jokes.Remove(jokes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jokesExists(int id)
        {
            return _context.jokes.Any(e => e.ID == id);
        }
    }
}
