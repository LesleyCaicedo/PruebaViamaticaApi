using CapaEntidades.Dtos;
using CapaEntidades.Modelos;
using Riok.Mapperly.Abstractions;

namespace CapaEntidades.Mapeador
{
    [Mapper]
    public partial class UsuarioMapper
    {
        public partial UsuarioDto Usuario_UsuarioDto(Usuario usuario);
        public partial Usuario UsuarioDto_Usuario(UsuarioDto usuarioDto);
    }
}
