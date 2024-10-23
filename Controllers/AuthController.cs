using CapaEntidades.Dtos;
using CapaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;
using PruebaViamaticaApi.Helpers;

namespace PruebaViamaticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AuthHelper _authHelper;

        public AuthController(IAuthService authService, IConfiguration configuration) 
        {
            _authService = authService;
            _authHelper = new(configuration);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Registro([FromBody] PersonaDto personaDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(await _authService.Registro(personaDto));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> IniciarSesion(string usuario_correo, string clave)
        {
            // USA SIEMPRE MODELOS O DTO's Y NO USE OBJETOS ANONIMOS PARA QUE NO TENGAS QUE HACER
            // MIS TONTERIAS DE DESERIALIZAR EL BENDITO OBJETO COMO AQUI ABAJO!!!!!!!!!!!!!!!!

            var resultado = await _authService.IniciarSesion(usuario_correo, clave);
            var tipo = resultado.GetType();

            var propRespuesta = tipo.GetProperty("respuesta");
            var respuestaValor = propRespuesta.GetValue(resultado, null);

            if (respuestaValor.ToString().Equals("Sesion iniciada.")) 
            {
                var propUsuario = tipo.GetProperty("usuario");
                var usuarioValor = propUsuario.GetValue(resultado, null);

                var tipoUsuario = usuarioValor.GetType();
                var usuario1 = tipoUsuario.GetProperty("Usuario1").GetValue(usuarioValor, null);
                var correo = tipoUsuario.GetProperty("Correo").GetValue(usuarioValor, null);
                var role = tipoUsuario.GetProperty("rol").GetValue(usuarioValor, null);

                respuestaValor = _authHelper.GenerateJWTToken(usuario1.ToString(), role.ToString(), correo.ToString());
            }

            return Ok(respuestaValor);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CerrarSesion(int idUsuario)
        {
            return Ok(await _authService.CerrarSesion(idUsuario));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DesbloquarUsuario(int idUsuario)
        {
            return Ok(await _authService.DesbloquearUsuario(idUsuario));
        }
    }
}
