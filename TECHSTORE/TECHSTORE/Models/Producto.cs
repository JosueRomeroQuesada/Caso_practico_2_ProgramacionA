using System;
using System.Collections.Generic;

namespace TECHSTORE.Models;

public partial class Producto
{
    public string NombreProducto { get; set; } = null!;

    public string IdCategoria { get; set; } = null!;

    public string IdMarca { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
