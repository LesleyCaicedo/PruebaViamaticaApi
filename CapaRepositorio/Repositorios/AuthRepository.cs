using CapaEntidades.Dtos;
using CapaEntidades.Mapeador;
using CapaEntidades.Modelos;
using CapaRepositorio.Database;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CapaRepositorio.Repositorios
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PruebaViamaticaContext _context;
        private readonly UsuarioMapper _usuarioMapper = new();

        public AuthRepository(PruebaViamaticaContext context)
        {
            _context = context;
        }

        public async Task<string> Registro(PersonaDto personaDto)
        {
            string respuesta = string.Empty;

            try
            {
                string vNombre = await ValidarNombreUsuario(personaDto.Usuario1!);
                string vClave = ValidarClave(personaDto.Clave!);
                string vCedula = ValidarCedula(personaDto.Cedula!);

                if (!vNombre.Equals("Validacion exitosa."))
                {
                    throw new Exception(vNombre);
                }
                else
                {
                    if (!vClave.Equals("Validacion exitosa."))
                    {
                        throw new Exception(vClave);
                    }
                    else
                    {
                        if (!vCedula.Equals("Validacion exitosa."))
                        {
                            throw new Exception(vCedula);
                        }
                    }
                }


                // Verificar si el nombre de usuario o correo ya existe
                var existingAccount = await _context.Usuarios
                    .FirstOrDefaultAsync(a => a.Usuario1 == personaDto.Usuario1 || a.Correo == personaDto.Correo);

                if (existingAccount != null)
                {
                    throw new Exception("El nombre de usuario o correo ya existe.");
                }

                // Crear una nueva instancia de Persona
                var nuevaPersona = new Persona
                {
                    Nombre = personaDto.Nombre,
                    Apellido = personaDto.Apellido,
                    Cedula = personaDto.Cedula,
                    FechaNacimiento = personaDto.FechaNacimiento
                };

                // Inicia una transacción
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        // Añadir la nueva persona al contexto
                        await _context.Personas.AddAsync(nuevaPersona);

                        // Guardar cambios para obtener el Id generado de Persona
                        await _context.SaveChangesAsync();

                        // Obtener el IdPersona generado
                        var idPersonaGenerado = nuevaPersona.IdPersona;

                        // Crear una nueva instancia de Usuario
                        var nuevoUsuario = new Usuario
                        {
                            Usuario1 = personaDto.Usuario1,
                            Correo = await GenerarCorreo(nuevaPersona.Nombre, nuevaPersona.Apellido),
                            Clave = personaDto.Clave,
                            SesionActiva = "I",
                            FkIdPersona = idPersonaGenerado, // Establecer la relación con el Id generado
                            Estatus = "Activo",
                            IntentosTemp = 0,
                            IntentosTotal = 0
                        };

                        // Añadir el nuevo usuario al contexto
                        await _context.Usuarios.AddAsync(nuevoUsuario);

                        // Guardar los cambios en la base de datos
                        await _context.SaveChangesAsync();

                        // Confirmar la transacción
                        await transaction.CommitAsync();

                        respuesta = $"Registro exitoso. Tu correo generado es {nuevoUsuario.Correo}";
                    }
                    catch (Exception ex)
                    {
                        // Revertir los cambios en caso de error
                        await transaction.RollbackAsync();

                        respuesta = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }


        public async Task<string> GenerarCorreo(string nombre, string apellido)
        {
            // Formatear el correo base (sin secuencia)
            string correoBase = $"{nombre[0].ToString().ToLower()}{apellido.ToLower()}";
            string dominio = "@mail.com";
            string correo = $"{correoBase}{dominio}";

            // Verificar si el correo ya existe
            int contador = 1;
            while (await _context.Usuarios.AnyAsync(u => u.Correo == correo))
            {
                // Si existe, genera un nuevo correo con un número al final
                correo = $"{correoBase}{contador}{dominio}";
                contador++;
            }

            return correo;
        }


        public async Task<object> IniciarSesion(string usuario_correo, string clave)
        {
            string respuesta = string.Empty;
            Usuario usuario = null;

            try
            {
                // Permite el ingreso con el username o el correo
                if (IsValidEmail(usuario_correo))
                {
                    usuario = await _context.Usuarios.FirstOrDefaultAsync(a => a.Correo == usuario_correo);

                    if (usuario == null)
                    {
                        throw new Exception("Correo no existe.");
                    }

                    if (!usuario.Estatus.Equals("Bloqueado"))
                    {
                        if (usuario.Clave.Equals(clave))
                        {
                            _context.Database.ExecuteSqlInterpolated($"EXEC ControlSesion {"SesionIniciada"}, {usuario.IdUsuario}");

                            usuario.IntentosTemp = 0;
                            usuario.SesionActiva = "A";
                            await _context.SaveChangesAsync();

                            respuesta = "Sesion iniciada.";
                        }
                        else
                        {
                            if (usuario.IntentosTemp < 3)
                            {
                                usuario.IntentosTemp += 1;
                                usuario.IntentosTotal += 1;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                usuario.Estatus = "Bloqueado";
                                await _context.SaveChangesAsync();
                            }

                            throw new Exception("Clave incorrecta.");
                        }
                    }
                    else
                    {
                        throw new Exception("No se puedo iniciar sesion, el usuario se encuentra bloqueado.");
                    }
                }
                else
                {
                    usuario = await _context.Usuarios.FirstOrDefaultAsync(a => a.Usuario1 == usuario_correo);

                    if (usuario == null)
                    {
                        throw new Exception("Usuario no existe.");
                    }

                    if (!usuario.Estatus.Equals("Bloqueado")) 
                    {
                        if (usuario.Clave.Equals(clave))
                        {
                            _context.Database.ExecuteSqlInterpolated($"EXEC ControlSesion {"SesionIniciada"}, {usuario.IdUsuario}");

                            usuario.IntentosTemp = 0;
                            usuario.SesionActiva = "A";
                            await _context.SaveChangesAsync();
                            respuesta = "Sesion iniciada.";

                        }
                        else
                        {
                            if (usuario.IntentosTemp < 3)
                            {
                                usuario.IntentosTemp += 1;
                                usuario.IntentosTotal += 1;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                usuario.Estatus = "Bloqueado";
                                await _context.SaveChangesAsync();
                            }

                            throw new Exception("Clave incorrecta.");
                        }
                    }
                    else
                    {
                        throw new Exception("No se puedo iniciar sesion, el usuario se encuentra bloqueado.");
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return new { respuesta, usuario = new { usuario.IdUsuario, usuario.Usuario1, usuario.Correo, usuario.SesionActiva, usuario.Estatus } };
        }

        public async Task<string> CerrarSesion(int usuarioId)
        {
            string respuesta = string.Empty;

            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);

                if (usuario != null || usuario.SesionActiva != "I")
                {
                    _context.Database.ExecuteSqlInterpolated($"EXEC ControlSesion {"SesionCerrada"}, {usuario.IdUsuario}");
                    respuesta = "Sesion cerrada con exito.";
                }
                else
                {
                    throw new Exception("Usuario no existe, o ya tiene la sesion cerrada.");
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }

        public async Task<string> DesbloquearUsuario(int idUsuario)
        {
            string respuesta = string.Empty;

            try
            {
                Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.IdUsuario == idUsuario);

                if (usuario != null)
                {
                    usuario.Estatus = "Activo";
                    usuario.IntentosTemp = 0;
                    await _context.SaveChangesAsync();

                    respuesta = "Usuario desbloqueado con exito.";
                }
                else 
                {
                    throw new Exception("El usuario no existe");
                }
            }
            catch (Exception ex) 
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Expresión regular para validar el correo electrónico
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }

        // Retorna true si esta todo correcto
        public async Task<string> ValidarNombreUsuario(string username)
        {

            // 1. Validar que la longitud esté entre 8 y 20 caracteres
            if (username.Length < 8 || username.Length > 20)
            {
                return "El nombre de usuario debe tener entre 8 y 20 caracteres.";
            }

            // 2. Verificar que no contenga signos (solo letras y números permitidos)
            if (!username.All(char.IsLetterOrDigit))
            {
                return "El nombre de usuario no debe contener signos.";
            }

            // 3. Validar que tenga al menos un número
            if (!username.Any(char.IsDigit))
            {
                return "El nombre de usuario debe contener al menos un número.";
            }

            // 4. Validar que tenga al menos una letra mayúscula
            if (!username.Any(char.IsUpper))
            {
                return "El nombre de usuario debe contener al menos una letra mayúscula.";
            }

            // 5. Verificar si el nombre de usuario ya existe en la base de datos
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(a => a.Usuario1 == username);

            if (existingUser != null)
            {
                return "El nombre de usuario ya está en uso.";
            }

            // Si pasa todas las validaciones
            return "Validacion exitosa.";
        }

        public string ValidarClave(string clave)
        {
            // 1. Validar que la longitud sea de al menos 8 caracteres
            if (clave.Length < 8)
            {
                return "La contraseña debe tener al menos 8 caracteres.";
            }

            // 2. Validar que contenga al menos una letra mayúscula
            if (!clave.Any(char.IsUpper))
            {
                return "La contraseña debe contener al menos una letra mayúscula.";
            }

            // 3. Verificar que no contenga espacios
            if (clave.Contains(' '))
            {
                return "La contraseña no debe contener espacios.";
            }

            // 4. Validar que contenga al menos un signo (no letras ni números)
            if (!clave.Any(c => !char.IsLetterOrDigit(c)))
            {
                return "La contraseña debe contener al menos un signo (carácter especial).";
            }

            // Si pasa todas las validaciones
            return "Validacion exitosa.";
        }

        public string ValidarCedula(string identification)
        {
            // 1. Validar que la identificación tenga exactamente 10 dígitos
            if (identification.Length != 10)
            {
                return "La identificación debe tener exactamente 10 dígitos.";
            }

            // 2. Verificar que solo contenga números
            if (!identification.All(char.IsDigit))
            {
                return "La identificación solo debe contener números.";
            }

            // 3. Validar que no tenga 4 dígitos repetidos consecutivos
            for (int i = 0; i < identification.Length - 3; i++)
            {
                if (identification[i] == identification[i + 1] &&
                    identification[i] == identification[i + 2] &&
                    identification[i] == identification[i + 3])
                {
                    return "La identificación no debe contener 4 números consecutivos iguales.";
                }
            }

            // Si pasa todas las validaciones
            return "Validacion exitosa.";
        }


    }

    public interface IAuthRepository
    {
        Task<string> Registro(PersonaDto personaDto);
        Task<object> IniciarSesion(string usuario_correo, string clave);
        Task<string> CerrarSesion(int usuarioId);
        Task<string> DesbloquearUsuario(int idUsuario);
    }
}
