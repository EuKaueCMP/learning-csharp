using BancoAPI.Applications.Services;
using BancoAPI.DTOs.UsuarioDTO;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<ListarUsuarioDTO>>> Listar()
        {
            try
            {
                return Ok(await _service.Listar());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("UsuarioId/{usuarioId}")]
        public async Task<ActionResult<ListarUsuarioDTO>> ObterUsuarioPorId(int usuarioId)
        {
            try
            {
                return Ok(await _service.ObterUsuarioPorId(usuarioId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("UsuarioEmail/{usuarioEmail}")]
        public async Task<ActionResult<ListarUsuarioDTO>> ObterUsuarioPorEmail(string usuarioEmail)
        {
            try
            {
                return Ok(await _service.ObterUsuarioPorEmail(usuarioEmail));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //TODO Fazendo os endpoints de inserção 
        //TODO Update e remoção de usuarios no banco
        // [HttpPost]
        // public async Task<
    }
}