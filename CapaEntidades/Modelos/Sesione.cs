using System;
using System.Collections.Generic;

namespace CapaEntidades.Modelos;

public partial class Sesione
{
    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaCierre { get; set; }

    public int FkIdUsuario { get; set; }

    public virtual Usuario FkIdUsuarioNavigation { get; set; } = null!;
}
