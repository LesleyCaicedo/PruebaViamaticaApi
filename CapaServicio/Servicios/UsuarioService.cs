using CapaRepositorio.Repositorios;

namespace CapaServicio.Servicios
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {

        public async Task<object> ObtenerInfo(int idUsuario) 
        {
            return await usuarioRepository.ObtenerInfo(idUsuario);
        }
    }

    public interface IUsuarioService
    {
        Task<object> ObtenerInfo(int idUsuario);
    }
}
