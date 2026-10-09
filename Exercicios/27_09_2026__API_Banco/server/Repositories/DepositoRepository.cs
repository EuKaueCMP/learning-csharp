using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly AppDbContext _ctx;
        public DepositoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<deposito>> Listar() => await _ctx.deposito.ToListAsync();

        public async Task<List<deposito>> ListarDepositoPorUsuarioId(int usuarioId) => await _ctx.deposito.Where(d => d.usuario_id == usuarioId).ToListAsync();
        
        public async Task<deposito> ObterDepositoPorId(int depositoId) => await _ctx.deposito.Include(d => d.usuario).Where(d => d.deposito_id == depositoId).FirstAsync();

        public async Task Depositar(deposito deposito)
        {
            if(deposito == null)
                return;

            deposito novoDeposito = new deposito
            {
                usuario_id = deposito.usuario_id,
                valor = deposito.valor,
                tipo_deposito = deposito.tipo_deposito,
                status_movimentacao = deposito.status_movimentacao,
                data_deposito = deposito.data_deposito,
            };

            await _ctx.deposito.AddAsync(novoDeposito);
            await _ctx.SaveChangesAsync();
        }
    }
}