using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SosD.Models;
using SosD.Repos;

namespace SosD.Controllers
{
    public class DiseñoController : Controller
    {
        private readonly SosDContext _context;

        public DiseñoController(SosDContext context)
        {
            _context = context;
        }

        // GET: Diseño
        public async Task<IActionResult> Index()
        {
              return _context.Diseño != null ? 
                          View(await _context.Diseño.ToListAsync()) :
                          Problem("Entity set 'SosDContext.Diseño'  is null.");
        }

        // GET: Diseño/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diseño == null)
            {
                return NotFound();
            }

            var diseño = await _context.Diseño
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseño == null)
            {
                return NotFound();
            }

            return View(diseño);
        }

        // GET: Diseño/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diseño/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Diseño diseño)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseño);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diseño);
        }

        // GET: Diseño/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diseño == null)
            {
                return NotFound();
            }

            var diseño = await _context.Diseño.FindAsync(id);
            if (diseño == null)
            {
                return NotFound();
            }
            return View(diseño);
        }

        // POST: Diseño/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Diseño diseño)
        {
            if (id != diseño.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseño);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseñoExists(diseño.Id))
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
            return View(diseño);
        }

        // GET: Diseño/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diseño == null)
            {
                return NotFound();
            }

            var diseño = await _context.Diseño
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseño == null)
            {
                return NotFound();
            }

            return View(diseño);
        }

        // POST: Diseño/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diseño == null)
            {
                return Problem("Entity set 'SosDContext.Diseño'  is null.");
            }
            var diseño = await _context.Diseño.FindAsync(id);
            if (diseño != null)
            {
                _context.Diseño.Remove(diseño);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseñoExists(int id)
        {
          return (_context.Diseño?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
