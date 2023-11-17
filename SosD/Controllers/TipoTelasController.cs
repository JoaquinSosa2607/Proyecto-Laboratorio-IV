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
    public class TipoTelasController : Controller
    {
        private readonly SosDContext _context;

        public TipoTelasController(SosDContext context)
        {
            _context = context;
        }

        // GET: TipoTelas
        public async Task<IActionResult> Index()
        {
              return _context.TipoTelas != null ? 
                          View(await _context.TipoTelas.ToListAsync()) :
                          Problem("Entity set 'SosDContext.TipoTelas'  is null.");
        }

        // GET: TipoTelas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoTelas == null)
            {
                return NotFound();
            }

            var tipoTela = await _context.TipoTelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTela == null)
            {
                return NotFound();
            }

            return View(tipoTela);
        }

        // GET: TipoTelas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoTelas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] TipoTela tipoTela)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoTela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTela);
        }

        // GET: TipoTelas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoTelas == null)
            {
                return NotFound();
            }

            var tipoTela = await _context.TipoTelas.FindAsync(id);
            if (tipoTela == null)
            {
                return NotFound();
            }
            return View(tipoTela);
        }

        // POST: TipoTelas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] TipoTela tipoTela)
        {
            if (id != tipoTela.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoTela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTelaExists(tipoTela.Id))
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
            return View(tipoTela);
        }

        // GET: TipoTelas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoTelas == null)
            {
                return NotFound();
            }

            var tipoTela = await _context.TipoTelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTela == null)
            {
                return NotFound();
            }

            return View(tipoTela);
        }

        // POST: TipoTelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoTelas == null)
            {
                return Problem("Entity set 'SosDContext.TipoTelas'  is null.");
            }
            var tipoTela = await _context.TipoTelas.FindAsync(id);
            if (tipoTela != null)
            {
                _context.TipoTelas.Remove(tipoTela);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTelaExists(int id)
        {
          return (_context.TipoTelas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
