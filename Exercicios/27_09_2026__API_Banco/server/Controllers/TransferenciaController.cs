using BancoAPI.Applications.Services;
using BancoAPI.DTOs.TransferenciaDTO;
using Microsoft.AspNetCore.Mvc;
using BancoAPI.Exceptions;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TransferenciaController : ControllerBase
    {
        private readonly TransferenciaService _service;
        public TransferenciaController(TransferenciaService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ListarTransferenciaDTO>> Listar()
        {
            try
            {
                return Ok(await _service.Listar());
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usuarioId/{usuarioId}")]
        public async Task<ActionResult<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioId(int usuarioId)
        {
            try
            {
                return Ok(await _service.ObterPorUsuarioId(usuarioId));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usuarioId/{usuarioId}/dataTransferencia/{data}")]
        public async Task<ActionResult<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioIdData(int usuarioId, DateOnly dataTransferencia)
        {
            try
            {
                return Ok(_service.ObterTransfrerenciaPorUsurioIdData(usuarioId, dataTransferencia));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usuarioId/{usuarioId}/tipoTransferencia/{tipoTransferencia}")]
        public async Task<ActionResult<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioIdTipoTransferencia(int usuarioId, string tipoTransferencia)
        {
            try
            {
                return Ok(_service.ObterTransferenciaPorUsuarioIdTipo(usuarioId, tipoTransferencia));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usuarioId/{usuarioId}/statusTransferencia/{statusTransferencia}")]
        public async Task<ActionResult<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioIdTipo(int usuarioId, string statusTransferencia)
        {
            try
            {
                return Ok(_service.ObteTransferenciaPorUsuarioIdStatus(usuarioId, statusTransferencia));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ListarTransferenciaDTO>> Transferir([FromBody] string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor, DateOnly dataTrasnferencia)
        {
            try
            {
                return Ok(_service.Transferir(tipoTransferencia, usuarioRemetenteId, usuarioDestinatarioId, valor));
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}