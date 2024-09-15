using CapaEntidades.Dtos;
using CapaRepositorio.Repositorios;

namespace CapaServicio.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> Registro(PersonaDto personaDto) 
        {
            return await _authRepository.Registro(personaDto);
        }

        public async Task<object> IniciarSesion(string usuario_correo, string clave)
        {
            return await _authRepository.IniciarSesion(usuario_correo, clave);
        }

        public async Task<string> CerrarSesion(int usuarioId)
        {
            return await _authRepository.CerrarSesion(usuarioId);
        }

        public async Task<string> DesbloquearUsuario(int idUsuario) 
        {
            return await _authRepository.DesbloquearUsuario(idUsuario);
        }
    }

    public interface IAuthService
    {
        Task<string> Registro(PersonaDto personaDto);
        Task<object> IniciarSesion(string usuario_correo, string clave);
        Task<string> CerrarSesion(int usuarioId);
        Task<string> DesbloquearUsuario(int idUsuario);
    }
}
