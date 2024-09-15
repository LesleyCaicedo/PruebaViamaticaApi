
using CapaEntidades.Modelos;
using CapaRepositorio.Database;
using Microsoft.EntityFrameworkCore;

namespace CapaRepositorio.Repositorios
{
    public class UsuarioRepository(PruebaViamaticaContext context) : IUsuarioRepository
    {

        public async Task<object> ObtenerInfo(int idUsuario) 
        {
            string respuesta = string.Empty;
            Usuario usuario = null; Sesione sesion = null;

            try
            {
                usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

                if (usuario == null) 
                {
                    throw new Exception("Usuario no existe");
                }

                sesion = await context.Sesiones.FirstOrDefaultAsync(x => x.FkIdUsuario == usuario.IdUsuario);

                respuesta = "Informacion del usuario encontrada.";
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            return new { respuesta, usuario, sesion };
        }
    }

    public interface IUsuarioRepository
    {
        Task<object> ObtenerInfo(int idUsuario);
    }
}
