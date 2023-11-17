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
    public class TipoPrendasController : Controller
    {
        private readonly SosDContext _context;

        public TipoPrendasController(SosDContext context)
        {
            _context = context;
        }

        // GET: TipoPrendas
        public async Task<IActionResult> Index()
        {
            var sosDContext = _context.TipoPrendas.Include(t => t.TipoTela);
            return View(await sosDContext.ToListAsync());
        }

        // GET: TipoPrendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoPrendas == null)
            {
                return NotFound();
            }

            var tipoPrenda = await _context.TipoPrendas
                .Include(t => t.TipoTela)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPrenda == null)
            {
                return NotFound();
            }

            return View(tipoPrenda);
        }

        // GET: TipoPrendas/Create
        public IActionResult Create()
        {
            ViewData["TipoTelaId"] = new SelectList(_context.TipoTelas, "Id", "Descripcion");
            return View();
        }

        // POST: TipoPrendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro,TipoTelaId")] TipoPrenda tipoPrenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPrenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoTelaId"] = new SelectList(_context.TipoTelas, "Id", "Descripcion", tipoPrenda.TipoTelaId);
            return View(tipoPrenda);
        }

        // GET: TipoPrendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoPrendas == null)
            {
                return NotFound();
            }

            var tipoPrenda = await _context.TipoPrendas.FindAsync(id);
            if (tipoPrenda == null)
            {
                return NotFound();
            }
            ViewData["TipoTelaId"] = new SelectList(_context.TipoTelas, "Id", "Descripcion", tipoPrenda.TipoTelaId);
            return View(tipoPrenda);
        }

        // POST: TipoPrendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro,TipoTelaId")] TipoPrenda tipoPrenda)
        {
            if (id != tipoPrenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPrenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPrendaExists(tipoPrenda.Id))
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
            ViewData["TipoTelaId"] = new SelectList(_context.TipoTelas, "Id", "Descripcion", tipoPrenda.TipoTelaId);
            return View(tipoPrenda);
        }

        // GET: TipoPrendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoPrendas == null)
            {
                return NotFound();
            }

            var tipoPrenda = await _context.TipoPrendas
                .Include(t => t.TipoTela)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPrenda == null)
            {
                return NotFound();
            }

            return View(tipoPrenda);
        }

        // POST: TipoPrendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoPrendas == null)
            {
                return Problem("Entity set 'SosDContext.TipoPrendas'  is null.");
            }
            var tipoPrenda = await _context.TipoPrendas.FindAsync(id);
            if (tipoPrenda != null)
            {
                _context.TipoPrendas.Remove(tipoPrenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPrendaExists(int id)
        {
          return (_context.TipoPrendas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
