using BancoAPI.DTOs.LogTransferenciaDTO;
using BancoAPI.Applications.Services;
using Microsoft.AspNetCore.Mvc;
using BancoAPI.Exceptions;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class LogTransferenciaController : ControllerBase
    {
        private readonly LogTransferenciaService _service;
        public LogTransferenciaController(LogTransferenciaService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ListarLogTransferenciaDTO>> Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListarLogTransferenciaDTO>> ObterPorId(int transferenciaId)
        {
            try
            {
                return Ok(await _service.ObterPorId(transferenciaId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterPorUsuarioId/{usuarioId}")]
        public async Task<ActionResult<ListarLogTransferenciaDTO>> ObterPorUsuarioId(int usuarioId)
        {
            try
            {
                return Ok(await _service.ObterPorUsuarioId(usuarioId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterPorUsuarioId/{usuarioId}/StatusTransferencia/{status}")]
        public async Task<ActionResult<ListarLogTransferenciaDTO>> ObterPorUsuarioIdStatus(int usuarioId, string status)
        {
            try
            {
                return Ok(_service.ObterPorUsuarioIdStatus(usuarioId, status));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}