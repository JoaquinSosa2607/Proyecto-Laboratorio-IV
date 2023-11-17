using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SosD.Models;
using SosD.Repos;

namespace SosD.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly SosDContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PromocionesController(SosDContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Promociones
        public async Task<IActionResult> Index()
        {
            var sosDContext = _context.Promociones.Include(p => p.MedioPago);
            return View(await sosDContext.ToListAsync());
        }

        // GET: Promociones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .Include(p => p.MedioPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // GET: Promociones/Create
        public IActionResult Create()
        {
            ViewData["MedioPagoId"] = new SelectList(_context.MedioPagos, "Id", "Descripcion");
            return View();
        }

        // POST: Promociones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CantMin,Descuento,MedioPagoId,FechaRegistro")] Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promociones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedioPagoId"] = new SelectList(_context.MedioPagos, "Id", "Descripcion", promociones.MedioPagoId);
            return View(promociones);
        }

        // GET: Promociones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones == null)
            {
                return NotFound();
            }
            ViewData["MedioPagoId"] = new SelectList(_context.MedioPagos, "Id", "Descripcion", promociones.MedioPagoId);
            return View(promociones);
        }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CantMin,Descuento,MedioPagoId,FechaRegistro")] Promociones promociones)
        {
            if (id != promociones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promociones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionesExists(promociones.Id))
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
            ViewData["MedioPagoId"] = new SelectList(_context.MedioPagos, "Id", "Descripcion", promociones.MedioPagoId);
            return View(promociones);
        }

        // GET: Promociones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .Include(p => p.MedioPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promociones == null)
            {
                return Problem("Entity set 'SosDContext.Promociones'  is null.");
            }
            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones != null)
            {
                _context.Promociones.Remove(promociones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionesExists(int id)
        {
          return (_context.Promociones?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //IMPORTAR EXCEL PROMOCIONES

        public IActionResult ImportarPromociones()
        {

            return View();
        }

        [HttpPost, ActionName("MostrarDatos")]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }

                ISheet HojaExcel = MiExcel.GetSheetAt(0);

                int cantidadFilas = HojaExcel.LastRowNum;

                List<Promociones> lista = new List<Promociones>();

                for (int i = 1; i <= cantidadFilas; i++)
                {

                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new Promociones
                    {
                        Descripcion = fila.GetCell(0).ToString(),
                        CantMin = Int16.Parse(fila.GetCell(1).ToString()),
                        Descuento = Int16.Parse(fila.GetCell(2).ToString()),
                        MedioPagoId = Int16.Parse(fila.GetCell(3).ToString()),
                        FechaRegistro = DateTime.Now

                    });
                }

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            else
            {

                return View();
            }

        }

        [HttpPost, ActionName("EnviarDatos")]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }

                ISheet HojaExcel = MiExcel.GetSheetAt(0);

                int cantidadFilas = HojaExcel.LastRowNum;
                List<Promociones> lista = new List<Promociones>();

                for (int i = 1; i <= cantidadFilas; i++)
                {

                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new Promociones
                    {
                        Descripcion = fila.GetCell(0).ToString(),
                        CantMin = Int16.Parse(fila.GetCell(1).ToString()),
                        Descuento = Int16.Parse(fila.GetCell(2).ToString()),
                        MedioPagoId = Int16.Parse(fila.GetCell(3).ToString()),
                        FechaRegistro = DateTime.Now

                    });
                }

                _context.BulkInsert(lista);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            else
            {
                return View();
            }
        }

        public IActionResult DownloadFile()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "archivos", "Promociones.xlsx");
            return File(System.IO.File.ReadAllBytes(filepath), "application/vnd.ms-excel", System.IO.Path.GetFileName(filepath));
        }

    }


}

