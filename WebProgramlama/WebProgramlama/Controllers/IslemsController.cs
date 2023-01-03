using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Data;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    [Authorize(Policy ="AdminOnly")]
    public class IslemsController : Controller
    {
        private readonly UserDbContext _context;

        public IslemsController(UserDbContext context)
        {
            _context = context;
        }

        // GET: Islems
        public async Task<IActionResult> Index()
        {
              return View(await _context.Islem.ToListAsync());
        }

        // GET: Islems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Islem == null)
            {
                return NotFound();
            }

            var islem = await _context.Islem
                .FirstOrDefaultAsync(m => m.IslemId == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // GET: Islems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Islems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IslemId,IslemAdi")] Islem islem)
        {
            islem.IslemId = _context.Islem.Count().ToString();
            if (ModelState["IslemAdi"].Errors.Count == 0)
            {
                _context.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(islem);
        }

        // GET: Islems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Islem == null)
            {
                return NotFound();
            }

            var islem = await _context.Islem.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }
            return View(islem);
        }

        // POST: Islems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IslemId,IslemAdi")] Islem islem)
        {
            if (id != islem.IslemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemExists(islem.IslemId))
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
            return View(islem);
        }

        // GET: Islems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Islem == null)
            {
                return NotFound();
            }

            var islem = await _context.Islem
                .FirstOrDefaultAsync(m => m.IslemId == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // POST: Islems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Islem == null)
            {
                return Problem("Entity set 'UserDbContext.Islem'  is null.");
            }
            var islem = await _context.Islem.FindAsync(id);
            if (islem != null)
            {
                _context.Islem.Remove(islem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslemExists(string id)
        {
          return _context.Islem.Any(e => e.IslemId == id);
        }
    }
}
