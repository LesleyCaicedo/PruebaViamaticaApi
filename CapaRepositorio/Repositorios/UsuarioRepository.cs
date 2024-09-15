
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

        public async Task<object> ObtenerIndicadores() 
        {
            string respuesta = string.Empty;
            int activos = 0; int inactivos = 0; int bloqueados = 0; int totalSesionesFallidas = 0;
            
            try
            {
                List<Usuario> usuarios = await context.Usuarios.ToListAsync();

                foreach (Usuario usuario in usuarios) 
                {
                    if (usuario.Estatus.Equals("Activo"))
                    {
                        activos++;
                    }
                    else if (usuario.Estatus.Equals("Inactivo"))
                    {
                        inactivos++;
                    }
                    else 
                    {
                        bloqueados++;
                    }

                    totalSesionesFallidas += usuario.IntentosTotal;
                }

                respuesta = "Indicadores consultados.";
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            return new { respuesta, activos, inactivos, bloqueados, totalSesionesFallidas };
        }
    }

    public interface IUsuarioRepository
    {
        Task<object> ObtenerInfo(int idUsuario);
        Task<object> ObtenerIndicadores();
    }
}
