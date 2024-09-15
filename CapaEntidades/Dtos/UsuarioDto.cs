
namespace CapaEntidades.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        public string? Usuario1 { get; set; }

        public string? Clave { get; set; }

        public string? Correo { get; set; }

        public string? SesionActiva { get; set; }

        public int FkIdPersona { get; set; }

        public string? Estatus { get; set; }
    }
}
