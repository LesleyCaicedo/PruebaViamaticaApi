using CapaRepositorio.Repositorios;

namespace CapaServicio.Servicios
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {

        public async Task<object> ObtenerInfo(int idUsuario) 
        {
            return await usuarioRepository.ObtenerInfo(idUsuario);
        }

        public async Task<object> ObtenerIndicadores()
        {
            return await usuarioRepository.ObtenerIndicadores();
        }

        public async Task<object> ObtenerUsuarios()
        {
            return await usuarioRepository.ObtenerUsuarios();
        }

        public async Task<object> CambiarEstadoUsuario(string estado, int idUsuario)
        {
            return await usuarioRepository.CambiarEstadoUsuario(estado, idUsuario);
        }

        public async Task<object> ObtenerUsuarioPorId(int idUsuario)
        {
            return await usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        }
    }

    public interface IUsuarioService
    {
        Task<object> ObtenerInfo(int idUsuario);
        Task<object> ObtenerIndicadores();
        Task<object> ObtenerUsuarios();
        Task<object> CambiarEstadoUsuario(string estado, int idUsuario);
        Task<object> ObtenerUsuarioPorId(int idUsuario);
    }
}
