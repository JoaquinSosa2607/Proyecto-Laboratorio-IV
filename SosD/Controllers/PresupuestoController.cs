using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SosD.Models;
using SosD.Repos;
using SosD.ViewModels;

namespace SosD.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly SosDContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PresupuestoController(SosDContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Presupuesto
        public async Task<IActionResult> Index()
        {
            var sosDContext = _context.Presupuestos.Include(p => p.Diseño).Include(p => p.TipoPrenda);
            return View(await sosDContext.ToListAsync());
        }

        // GET: Presupuesto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Presupuestos == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .Include(p => p.Diseño)
                .Include(p => p.TipoPrenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        // GET: Presupuesto/Create
        public IActionResult Create()
        {
            ViewData["DiseñoId"] = new SelectList(_context.Diseño, "Id", "Descripcion");
            ViewData["TipoPrendaId"] = new SelectList(_context.TipoPrendas, "Id", "Descripcion");
            return View();
        }

        // POST: Presupuesto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PresupuestoViewModel model)
        {

            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Presupuesto presupuesto = new Presupuesto()
                {
                    Descripcion= model.Descripcion,
                    Cantidad = model.Cantidad,
                    TipoPrendaId = model.TipoPrendaId,
                    ImagenPrenda = uniqueFileName,
                    DiseñoId = model.DiseñoId,
                    PrecioUni = model.PrecioUni,
                    FechaRegistro = model.FechaRegistro,

                };
                _context.Add(presupuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoPrendaId"] = new SelectList(_context.TipoPrendas, "Id", "Descripcion", model.TipoPrendaId);
            ViewData["DiseñoId"] = new SelectList(_context.Diseño, "Id", "Descripcion", model.DiseñoId);
            return View(model);
        }

        private string UploadedFile(PresupuestoViewModel model)
        {
            string uniqueFileName = null;

            if (model.Imagen != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagen.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagen.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        // GET: Presupuesto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Presupuestos == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos.FindAsync(id);

            PresupuestoViewModel presupuestoViewModel = new PresupuestoViewModel()
            {
                Descripcion = presupuesto.Descripcion,
                Cantidad = presupuesto.Cantidad,
                TipoPrenda = presupuesto.TipoPrenda,
                ImagenPrenda = presupuesto.ImagenPrenda,
                Diseño = presupuesto.Diseño,
                PrecioUni = presupuesto.PrecioUni,

            };

            if (presupuesto == null)
            {
                return NotFound();
            }
            ViewData["DiseñoId"] = new SelectList(_context.Diseño, "Id", "Descripcion", presupuesto.DiseñoId);
            ViewData["TipoPrendaId"] = new SelectList(_context.TipoPrendas, "Id", "Descripcion", presupuesto.TipoPrendaId);
            return View(presupuestoViewModel);
        }

        // POST: Presupuesto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PresupuestoViewModel model)
        {

            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var presupuesto = await _context.Presupuestos.FindAsync(id);


                    presupuesto.ImagenPrenda = uniqueFileName;
                    presupuesto.Descripcion = model.Descripcion;
                    presupuesto.Cantidad = model.Cantidad;
                    presupuesto.TipoPrendaId = model.TipoPrendaId;
                    presupuesto.DiseñoId = model.DiseñoId;
                    presupuesto.FechaRegistro = model.FechaRegistro;
                    presupuesto.PrecioUni = model.PrecioUni;
                   

                    _context.Update(presupuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresupuestoExists(model.Id))
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
            ViewData["DiseñoId"] = new SelectList(_context.Diseño, "Id", "Descripcion", model.DiseñoId);
            ViewData["TipoPrendaId"] = new SelectList(_context.TipoPrendas, "Id", "Descripcion", model.TipoPrendaId);
            return View(model);
        }

        // GET: Presupuesto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Presupuestos == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .Include(p => p.Diseño)
                .Include(p => p.TipoPrenda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        // POST: Presupuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Presupuestos == null)
            {
                return Problem("Entity set 'SosDContext.Presupuestos'  is null.");
            }
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresupuestoExists(int id)
        {
          return (_context.Presupuestos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
