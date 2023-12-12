using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TECHSTORE.Models;

namespace TECHSTORE.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly TechstoreContext _context;

        public VendedoresController(TechstoreContext context)
        {
            _context = context;
        }

        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
              return _context.Vendedores != null ? 
                          View(await _context.Vendedores.ToListAsync()) :
                          Problem("Entity set 'TechstoreContext.Vendedores'  is null.");
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.NombreVendedor == id);
            if (vendedore == null)
            {
                return NotFound();
            }

            return View(vendedore);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreVendedor")] Vendedore vendedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedore);
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores.FindAsync(id);
            if (vendedore == null)
            {
                return NotFound();
            }
            return View(vendedore);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreVendedor")] Vendedore vendedore)
        {
            if (id != vendedore.NombreVendedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedoreExists(vendedore.NombreVendedor))
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
            return View(vendedore);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.NombreVendedor == id);
            if (vendedore == null)
            {
                return NotFound();
            }

            return View(vendedore);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vendedores == null)
            {
                return Problem("Entity set 'TechstoreContext.Vendedores'  is null.");
            }
            var vendedore = await _context.Vendedores.FindAsync(id);
            if (vendedore != null)
            {
                _context.Vendedores.Remove(vendedore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedoreExists(string id)
        {
          return (_context.Vendedores?.Any(e => e.NombreVendedor == id)).GetValueOrDefault();
        }
    }
}
