using BancoApi.Services;
using BancoAPI.Domains;
using BancoAPI.DTOs.SaqueDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Controllers
{
    [Route("api/[controllers]")]
    public class SaqueController : ControllerBase
    {
        private readonly SaqueService _service;
        public SaqueController(SaqueService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult> Listar()
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

        [HttpGet("SaquePorNumeroContaAgencia")]
        public async Task<ActionResult> ListarSaqueContaAgencia([FromBody] long numeroConta, string agencia)
        {
            try
            {
                return Ok(await _service.ListarSaquePorAgenciaConta(numeroConta, agencia));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet("SaquePorId/{saqueId}")]
        public async Task<ActionResult> ObterSaquePorId(int saqueId)
        {
            try
            {
                return Ok(await _service.ObterSaquePorId(saqueId));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EfetuarSaque(string agenciaConta, long numeroConta, decimal valor, string localizacaoSaque, tipo_saque_enum tipo)
        {
            try
            {
                EfetuarSaqueDto novoSaque = new EfetuarSaqueDto
                {
                    AgenciaConta = agenciaConta,
                    NumeroConta = numeroConta,
                    Valor = valor,
                    LocalizacaoSaque = localizacaoSaque,
                    TipoSaque = tipo,
                    StatusMovimentacao = status_movimentacao_enum.EM_ANDAMENTO
                };

                await _service.Sacar(novoSaque);
                //TODOOOO Fazer uma funcão para adicionar um novo status de concluido aqui antes de postar    

                return StatusCode(201, "Saque concluido");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}