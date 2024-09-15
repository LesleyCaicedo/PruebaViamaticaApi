using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace CapaEntidades.Modelos;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Cedula { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    //public Usuario Usuario { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
