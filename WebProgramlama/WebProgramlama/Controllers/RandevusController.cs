using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Areas.Identity.Data;
using WebProgramlama.Data;
using WebProgramlama.Models;

namespace WebProgramlama.Controllers
{
    public class RandevusController : Controller
    {
        private readonly UserDbContext _context;

        public RandevusController(UserDbContext context)
        {
            _context = context;
        }

        // GET: Randevus
        public async Task<IActionResult> Index()
        {
            var userDbContext = _context.Randevu.Include(r => r.Islem).Include(r => r.User);
            return View(await userDbContext.ToListAsync());
        }

        // GET: Randevus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.Islem)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.randevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevus/Create
        public IActionResult Create()
        {
            List<Islem> islem= new List<Islem>();
            islem = _context.Islem.ToList();
            TempData["islem"] = islem;
            return View();
        }

        // POST: Randevus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("randevuId,randevuZamani,userId,plaka,islemId")] Randevu randevu)
        {
            randevu.userId = _context.Users.FirstOrDefault(d => d.UserName == User.Identity.Name.ToString()).Id;
            if(randevu.randevuZamani>DateTime.Now&&randevu.plaka.Length<8)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["islemId"] = new SelectList(_context.Islem, "IslemId", "IslemId", randevu.islemId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", randevu.userId);
            return View(randevu);
        }

        // GET: Randevus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["islemId"] = new SelectList(_context.Islem, "IslemId", "IslemId", randevu.islemId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", randevu.userId);
            return View(randevu);
        }

        // POST: Randevus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("randevuId,randevuZamani,userId,plaka,islemId")] Randevu randevu)
        {
            if (id != randevu.randevuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.randevuId))
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
            ViewData["islemId"] = new SelectList(_context.Islem, "IslemId", "IslemId", randevu.islemId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", randevu.userId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.Islem)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.randevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevu == null)
            {
                return Problem("Entity set 'UserDbContext.Randevu'  is null.");
            }
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevu.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return _context.Randevu.Any(e => e.randevuId == id);
        }
    }
}
