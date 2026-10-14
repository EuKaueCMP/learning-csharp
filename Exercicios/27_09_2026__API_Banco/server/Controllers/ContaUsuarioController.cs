using BancoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContaUsuarioController : ControllerBase
    {
        private readonly ContaUsuarioService _service;
        public ContaUsuarioController(ContaUsuarioService service) => _service = service;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ListarContas()
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

        [HttpGet("BuscarConta")]
        public async Task<ActionResult> BuscarContaUsuario(long numeroConta, string agenciaConta)
        {
            try
            {
                return Ok(await _service.ObterContaPorNumeroContaAgencia(numeroConta, agenciaConta));
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ConsultarSaldo")]
        public async Task<ActionResult> ConsultarSaldoUsuario([FromBody] long numeroConta, string agenciaConta)
        {
            try
            {
                return Ok(await _service.ObterSaldo(numeroConta, agenciaConta));
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }
    }
}