using CapaEntidades.Dtos;
using CapaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace PruebaViamaticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
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
            return Ok(await _authService.IniciarSesion(usuario_correo, clave));
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
