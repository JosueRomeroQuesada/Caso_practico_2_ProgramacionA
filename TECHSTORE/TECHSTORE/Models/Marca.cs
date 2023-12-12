using System;
using System.Collections.Generic;

namespace TECHSTORE.Models;

public partial class Marca
{
    public string NombreMarca { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
