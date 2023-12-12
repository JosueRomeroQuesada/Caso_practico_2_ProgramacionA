using System;
using System.Collections.Generic;

namespace TECHSTORE.Models;

public partial class Categoria
{
    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
