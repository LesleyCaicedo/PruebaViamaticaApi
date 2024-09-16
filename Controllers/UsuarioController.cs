using CapaServicio.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace PruebaViamaticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerInfo(int idUsuario)
        {
            return Ok(await usuarioService.ObtenerInfo(idUsuario));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerIndicadores()
        {
            return Ok(await usuarioService.ObtenerIndicadores());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            return Ok(await usuarioService.ObtenerUsuarios());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int idUsuario)
        {
            return Ok(await usuarioService.ObtenerUsuarioPorId(idUsuario));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CambiarEstadoUsuario(string estado, int idUsuario)
        {
            return Ok(await usuarioService.CambiarEstadoUsuario(estado, idUsuario));
        }
    }
}
