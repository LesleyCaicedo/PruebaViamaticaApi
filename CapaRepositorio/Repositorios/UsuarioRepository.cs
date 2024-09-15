
namespace CapaRepositorio.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<object> ObtenerInfo() 
        {
            string respuesta = string.Empty;

            return new { respuesta };
        }
    }

    public interface IUsuarioRepository
    {

    }
}
