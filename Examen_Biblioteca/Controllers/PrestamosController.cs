using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen_Biblioteca.Data;
using Examen_Biblioteca.Models;

namespace Examen_Biblioteca.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prestamos.Include(p => p.Libro).Include(p => p.Miembro);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Miembro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamos == null)
            {
                return NotFound();
            }

            return View(prestamos);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "NombreCompleto");
            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,MiembroId,FechaPrestamo,FechaDevolucion,Devuelto")] Prestamos prestamos)
        {
            if (prestamos.Id == 0)
            {
                _context.Add(prestamos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamos.LibroId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "NombreCompleto", prestamos.MiembroId);
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamos.LibroId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "NombreCompleto", prestamos.MiembroId);
            return View(prestamos);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,MiembroId,FechaPrestamo,FechaDevolucion,Devuelto")] Prestamos prestamos)
        {
            if (id != prestamos.Id)
            {
                return NotFound();
            }

            if (prestamos.Id != 0)
            {
                try
                {
                    _context.Update(prestamos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamosExists(prestamos.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamos.LibroId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "NombreCompleto", prestamos.MiembroId);
            return View(prestamos);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Miembro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamos == null)
            {
                return NotFound();
            }

            return View(prestamos);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos != null)
            {
                _context.Prestamos.Remove(prestamos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamosExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
