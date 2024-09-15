using System;
using System.Collections.Generic;

namespace CapaEntidades.Modelos;

public partial class RolUsuario
{
    public int FkIdRol { get; set; }

    public int FkIdUsuario { get; set; }

    public virtual Rol FkIdRolNavigation { get; set; } = null!;

    public virtual Usuario FkIdUsuarioNavigation { get; set; } = null!;
}
