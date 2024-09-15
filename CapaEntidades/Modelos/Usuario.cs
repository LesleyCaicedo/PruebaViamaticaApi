using System;
using System.Collections.Generic;

namespace CapaEntidades.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Clave { get; set; }

    public string? Correo { get; set; }

    public string? SesionActiva { get; set; }

    public int FkIdPersona { get; set; }

    public string? Estatus { get; set; }
    public int IntentosTemp { get; set; }
    public int IntentosTotal { get; set; }

    //public Persona Persona { get; set; }

    public virtual Persona FkIdPersonaNavigation { get; set; } = null!;
}
