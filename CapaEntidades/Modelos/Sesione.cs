using System;
using System.Collections.Generic;

namespace CapaEntidades.Modelos;

public partial class Sesione
{
    public DateOnly? FechaIngreso { get; set; }

    public DateOnly? FechaCierre { get; set; }

    public int FkIdUsuario { get; set; }

    public virtual Usuario FkIdUsuarioNavigation { get; set; } = null!;
}
