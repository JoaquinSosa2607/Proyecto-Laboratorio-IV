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
    public class MedioPagosController : Controller
    {
        private readonly SosDContext _context;

        public MedioPagosController(SosDContext context)
        {
            _context = context;
        }

        // GET: MedioPagos
        public async Task<IActionResult> Index()
        {
              return _context.MedioPagos != null ? 
                          View(await _context.MedioPagos.ToListAsync()) :
                          Problem("Entity set 'SosDContext.MedioPagos'  is null.");
        }

        // GET: MedioPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedioPagos == null)
            {
                return NotFound();
            }

            var medioPago = await _context.MedioPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medioPago == null)
            {
                return NotFound();
            }

            return View(medioPago);
        }

        // GET: MedioPagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedioPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] MedioPago medioPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medioPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medioPago);
        }

        // GET: MedioPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedioPagos == null)
            {
                return NotFound();
            }

            var medioPago = await _context.MedioPagos.FindAsync(id);
            if (medioPago == null)
            {
                return NotFound();
            }
            return View(medioPago);
        }

        // POST: MedioPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] MedioPago medioPago)
        {
            if (id != medioPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medioPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedioPagoExists(medioPago.Id))
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
            return View(medioPago);
        }

        // GET: MedioPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedioPagos == null)
            {
                return NotFound();
            }

            var medioPago = await _context.MedioPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medioPago == null)
            {
                return NotFound();
            }

            return View(medioPago);
        }

        // POST: MedioPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedioPagos == null)
            {
                return Problem("Entity set 'SosDContext.MedioPagos'  is null.");
            }
            var medioPago = await _context.MedioPagos.FindAsync(id);
            if (medioPago != null)
            {
                _context.MedioPagos.Remove(medioPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedioPagoExists(int id)
        {
          return (_context.MedioPagos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
