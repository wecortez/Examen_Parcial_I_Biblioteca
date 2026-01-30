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
    public class MiembrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MiembrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Miembros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Miembros.ToListAsync());
        }

        // GET: Miembros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembros = await _context.Miembros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembros == null)
            {
                return NotFound();
            }

            return View(miembros);
        }

        // GET: Miembros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miembros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,FechaSuscripcion")] Miembros miembros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miembros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miembros);
        }

        // GET: Miembros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembros = await _context.Miembros.FindAsync(id);
            if (miembros == null)
            {
                return NotFound();
            }
            return View(miembros);
        }

        // POST: Miembros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,FechaSuscripcion")] Miembros miembros)
        {
            if (id != miembros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembrosExists(miembros.Id))
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
            return View(miembros);
        }

        // GET: Miembros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembros = await _context.Miembros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembros == null)
            {
                return NotFound();
            }

            return View(miembros);
        }

        // POST: Miembros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembros = await _context.Miembros.FindAsync(id);
            if (miembros != null)
            {
                _context.Miembros.Remove(miembros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiembrosExists(int id)
        {
            return _context.Miembros.Any(e => e.Id == id);
        }
    }
}
