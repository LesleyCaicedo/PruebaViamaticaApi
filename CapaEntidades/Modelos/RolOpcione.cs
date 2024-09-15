using System;
using System.Collections.Generic;

namespace CapaEntidades.Modelos;

public partial class RolOpcione
{
    public int FkIdRol { get; set; }

    public int FkIdOpcion { get; set; }

    public virtual Opcione FkIdOpcionNavigation { get; set; } = null!;

    public virtual Rol FkIdRolNavigation { get; set; } = null!;
}
