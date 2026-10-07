using BancoAPI.Applications.Services;
using BancoAPI.DTOs.UsuarioLogDTO;
using BancoAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioLogController : ControllerBase
    {
        private readonly UsuarioLogService _service;
        public UsuarioLogController(UsuarioLogService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ListarUsuarioLogDTO>> Listar()
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

        [HttpGet("logId/[logId]")]
        public async Task<ActionResult<ListarUsuarioLogDTO>> ObterLogPorId(int logId)
        {
            try
            {
                return Ok(await _service.ObterPorId(logId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}