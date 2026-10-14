
using BancoAPI.Domains;
using BancoAPI.DTOs.PagamentoDTO;
using BancoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Controllers
{
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _service;
        public PagamentoController(PagamentoService service) => _service = service;

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

        [HttpGet("PagamentoPorAgenciaNumeroConta")]
        public async Task<ActionResult> ObterPagametosPorAgenciaConta([FromBody] string agencia, long numeroConta)
        {
            try
            {
                return Ok(await _service.ListarPagamentoPorAgenciaConta(numeroConta, agencia));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpGet("PagamentoId/{pagamentoId}")]
        public async Task<ActionResult> ObterPagamentoPorId(int pagamentoId)
        {
            try
            {
                return Ok(await _service.ObterPagamentoPorId(pagamentoId));
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Sacar([FromBody] long numeroConta, string agenciaConta, decimal valor, tipo_pagamento_enum tipo)
        {
            try
            {
                EfetuarPagamentoDto novoPagamento = new EfetuarPagamentoDto
                {
                    NumeroConta = numeroConta,
                    AgenciaConta = agenciaConta,
                    Valor = valor,
                    tipo_pagamento = tipo,
                    status_movimentacao = status_movimentacao_enum.EM_ANDAMENTO
                };

                await _service.EfetuarPagamento(novoPagamento);
                //TODOOOO adiiconar um metodo que atualiza o status do pagamento para CONCLUIDO...

                return StatusCode(201, "Pagamento efetuado com sucesso!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
