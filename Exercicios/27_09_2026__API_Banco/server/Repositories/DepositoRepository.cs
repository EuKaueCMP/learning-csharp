using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly AppDbContext _ctx;
        public DepositoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<Deposito>> Listar() => await _ctx.Deposito.ToListAsync();

        public async Task<List<Deposito>> ListarDepositoPorUsuarioId(int usuarioId) => await _ctx.Deposito.Where(d => d.NumeroContaNavigation.UsuarioId == usuarioId).ToListAsync();

        public async Task<Deposito> ObterDepositoPorId(int depositoId) => await _ctx.Deposito.Include(d => d.NumeroContaNavigation.Usuario).FirstAsync(d => d.DepositoId == depositoId);

        public async Task Depositar(int conta, string agencia, decimal valor, tipo_deposito_enum tipo, status_movimentacao_enum status, DateTime data)
        {
            ContaUsuario contaUsu = await _ctx.ContaUsuario.FirstAsync(c => c.NumeroConta == conta);
            if (valor <= 0 || contaUsu == null)
                return;

            Deposito novoDeposito = new Deposito
            {
                NumeroAgencia = agencia,
                NumeroConta = conta,
                Valor = valor,
                tipo_deposito = tipo,
                status_movimentacao = status,
                DataDeposito = data,
            };

            await _ctx.Deposito.AddAsync(novoDeposito);
            await _ctx.SaveChangesAsync();
        }
    }
}