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
    public class ProductoController : Controller
    {
        private readonly TechstoreContext _context;

        public ProductoController(TechstoreContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var techstoreContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdMarcaNavigation);
            return View(await techstoreContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.NombreProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProducto,IdCategoria,IdMarca,Precio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", producto.IdMarca);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", producto.IdMarca);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreProducto,IdCategoria,IdMarca,Precio")] Producto producto)
        {
            if (id != producto.NombreProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.NombreProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "NombreCategoria", "NombreCategoria", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", producto.IdMarca);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.NombreProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'TechstoreContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
          return (_context.Productos?.Any(e => e.NombreProducto == id)).GetValueOrDefault();
        }
    }
}
