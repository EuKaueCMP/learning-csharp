using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.Applications.Services;
using Microsoft.AspNetCore.Mvc;
using BancoAPI.Exceptions;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly MovimentacaoService _service;
        public MovimentacaoController(MovimentacaoService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ListarMovimentacaoDTO>> Listar()
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

        [HttpGet("ObterPorMovimentacaoId/{movimentacaoId}")]
        public async Task<ActionResult<ListarMovimentacaoDTO>> ObterPorMovimentacaoId(int movimentacaoId)
        {
            try
            {
                return Ok(await _service.ObterPorId(movimentacaoId));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        
        [HttpGet("ObterPorUsuarioId/{usuarioId}")]
        public async Task<ActionResult<ListarMovimentacaoDTO>> ObterPorUsuarioId(int usuarioId)
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

        public async Task<ActionResult<ListarMovimentacaoDTO>> ObterMovimentacaoPorData(DateOnly data)
        {
            try
            {
                return Ok(await _service.obterPorData(data));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ObterMovimentacaoPorUsuarioId/{usuarioId}/Data/{data}")]
        public async Task<ActionResult<ListarMovimentacaoDTO>> ObterMovimentacaoPorUsuarioIdData(int usuarioId, DateOnly data)
        {
            try
            {
                return Ok(await _service.ObterTransfrerenciaPorUsurioIdData(usuarioId, data));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}