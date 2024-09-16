
using CapaEntidades.Dtos;
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
            Usuario usuario = null; Sesione sesion = null; string rol = string.Empty;

            try
            {
                usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

                if (usuario == null) 
                {
                    throw new Exception("Usuario no existe");
                }

                RolUsuario rolUsuario = await context.RolUsuarios.FirstOrDefaultAsync(a => a.FkIdUsuario == usuario.IdUsuario);
                Rol role = await context.Rols.FirstAsync(a => a.IdRol == rolUsuario.FkIdRol);

                rol = role.NombreRol;

                sesion = await context.Sesiones.FirstOrDefaultAsync(x => x.FkIdUsuario == usuario.IdUsuario);

                respuesta = "Informacion del usuario encontrada.";
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            //return new { respuesta, usuario, sesion };
            return new { respuesta, usuario = new { usuario.Usuario1, usuario.Correo, usuario.Estatus, usuario.IntentosTotal, rol }, sesion };
        }

        public async Task<object> ObtenerUsuarios() 
        {
            string respuesta = string.Empty;
            PersonaDto personaDto = null;
            List<PersonaDto> listaPersonas = new();

            try
            {
                List<Usuario> usuarios = await context.Usuarios.ToListAsync();
                List<Persona> personas = await context.Personas.ToListAsync();

                foreach (Persona persona in personas) 
                {
                    personaDto = new()
                    {
                        IdPersona = persona.IdPersona,
                        Cedula = persona.Cedula,
                        Usuario1 = usuarios.FirstOrDefault(x => x.FkIdPersona == persona.IdPersona).Usuario1,
                        Nombre = persona.Nombre,
                        Apellido = persona.Apellido,
                        FechaNacimiento = persona.FechaNacimiento,
                        Correo = usuarios.FirstOrDefault(x => x.FkIdPersona == persona.IdPersona).Correo,
                        Estatus = usuarios.FirstOrDefault(x => x.FkIdPersona == persona.IdPersona).Estatus
                    };

                    listaPersonas.Add(personaDto);
                }

                respuesta = "Lista de usuarios encontrados.";
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            return new { respuesta, listaPersonas };
        }

        public async Task<object> ObtenerUsuarioPorId(int idUsuario)
        {
            string respuesta = string.Empty;
            PersonaDto persona = null;

            try
            {
                Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
                Persona personaef = await context.Personas.FirstOrDefaultAsync(x => x.IdPersona == usuario.FkIdPersona); ;

 
                    persona = new()
                    {
                        IdPersona = personaef.IdPersona,
                        Cedula = personaef.Cedula,
                        Usuario1 = usuario.Usuario1,
                        Nombre = personaef.Nombre,
                        Apellido = personaef.Apellido,
                        FechaNacimiento = personaef.FechaNacimiento,
                        Correo = usuario.Correo,
                        Estatus = usuario.Estatus
                    };


                respuesta = "Usuario encontrado.";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return new { respuesta, persona };
        }

        public async Task<object> CambiarEstadoUsuario(string estado, int idUsuario) 
        {
            string respuesta = string.Empty;

            try
            {
                Usuario usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

                if (usuario == null) 
                {
                    throw new Exception("Usuario no existe.");
                }

                if (estado.Equals("Activar"))
                {
                    usuario.Estatus = "Activo";
                    respuesta = "Usuario activado.";
                }
                else if (estado.Equals("Inactivar"))
                {
                    usuario.Estatus = "Inactivo";
                    respuesta = "Usuario eliminado.";
                }
                else if(estado.Equals("Bloquear"))
                {
                    usuario.Estatus = "Bloqueado";
                    respuesta = "Usuario bloqueado.";
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            return new { respuesta };
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
        Task<object> ObtenerUsuarios();
        Task<object> CambiarEstadoUsuario(string estado, int idUsuario);
        Task<object> ObtenerUsuarioPorId(int idUsuario);
    }
}
