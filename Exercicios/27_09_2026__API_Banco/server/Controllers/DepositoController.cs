using BancoAPI.Domains;
using BancoAPI.DTOs.DepositoDTO;
using BancoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers
{
    [Route("api/[controllers]")]
    public class DepositoController : ControllerBase
    {
        private readonly DepositoService _service;
        public DepositoController(DepositoService service) => _service = service;

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

        [HttpGet("DepositosPorUsuarioId/{usuarioId}")]
        public async Task<ActionResult> ObterDepositosPorUsuarioId(int usuarioId)
        {
            try
            {
                return Ok(await _service.ObterDepositoPorUsuarioId(usuarioId));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet("DepositoId/{depositoId}")]
        public async Task<ActionResult> ObterDepositoPorDepositoId(int depositoId)
        {
            try
            {
                return Ok(await _service.ObterDepositoPorId(depositoId));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DepositarSaldo([FromBody] long numeroConta, string agencia, decimal valor, tipo_deposito_enum tipo)
        {
            try
            {
                DepositarDto novoDeposito = new DepositarDto
                {
                    NumeroConta = numeroConta,
                    NumeroAgencia = agencia,
                    Valor = valor,
                    tipo_deposito = tipo,
                    status_movimentacao = status_movimentacao_enum.EM_ANDAMENTO
                };

                await _service.Depositar(novoDeposito);
                //TODOOOOO Acrescentar um metodo que atualiza o status para concluido
                // await _service.AtualizarStatus(novoDeposito)
                return StatusCode(201, "Deposito concluido!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}