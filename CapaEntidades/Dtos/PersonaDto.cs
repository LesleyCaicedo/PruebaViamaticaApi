namespace CapaEntidades.Dtos
{
    public class PersonaDto
    {
        public int IdPersona { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Cedula { get; set; }

        public DateOnly? FechaNacimiento { get; set; }
        public string? Usuario1 { get; set; }

        public string? Clave { get; set; }

        public string? Correo { get; set; }
        public string? Estatus { get; set; }
    }
}
