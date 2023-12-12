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
    public class VentasController : Controller
    {
        private readonly TechstoreContext _context;

        public VentasController(TechstoreContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var techstoreContext = _context.Ventas.Include(v => v.IdClienteNavigation).Include(v => v.NombreProductoNavigation).Include(v => v.NombreVendedorNavigation);
            return View(await techstoreContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.NombreProductoNavigation)
                .Include(v => v.NombreVendedorNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto");
            ViewData["NombreVendedor"] = new SelectList(_context.Vendedores, "NombreVendedor", "NombreVendedor");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,NombreVendedor,NombreProducto,FechaVenta,Cantidad,IdCliente")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", venta.IdCliente);
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", venta.NombreProducto);
            ViewData["NombreVendedor"] = new SelectList(_context.Vendedores, "NombreVendedor", "NombreVendedor", venta.NombreVendedor);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", venta.IdCliente);
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", venta.NombreProducto);
            ViewData["NombreVendedor"] = new SelectList(_context.Vendedores, "NombreVendedor", "NombreVendedor", venta.NombreVendedor);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,NombreVendedor,NombreProducto,FechaVenta,Cantidad,IdCliente")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", venta.IdCliente);
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", venta.NombreProducto);
            ViewData["NombreVendedor"] = new SelectList(_context.Vendedores, "NombreVendedor", "NombreVendedor", venta.NombreVendedor);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.NombreProductoNavigation)
                .Include(v => v.NombreVendedorNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'TechstoreContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
