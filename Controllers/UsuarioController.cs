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
    }
}
