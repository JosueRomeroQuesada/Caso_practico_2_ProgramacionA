using System;
using System.Collections.Generic;

namespace TECHSTORE.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string NombreVendedor { get; set; } = null!;

    public string NombreProducto { get; set; } = null!;

    public DateTime FechaVenta { get; set; }

    public int Cantidad { get; set; }

    public int IdCliente { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Producto NombreProductoNavigation { get; set; } = null!;

    public virtual Vendedore NombreVendedorNavigation { get; set; } = null!;
}
