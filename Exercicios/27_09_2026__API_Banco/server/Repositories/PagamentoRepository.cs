using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly AppDbContext _ctx;
        public PagamentoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<Pagamento>> Listar() => await _ctx.Pagamento.ToListAsync();

        public async Task<List<Pagamento>> ListarPagamentosPorAgenciaConta(int numeroConta, string agencia) => await _ctx.Pagamento.Where(s => s.NumeroConta == numeroConta && s.AgenciaConta == agencia).ToListAsync();

        public async Task<Pagamento> ObterPagamentoPorId(int pagamentoId) => await _ctx.Pagamento.FirstAsync(s => s.PagamentoId == pagamentoId);

        public async Task Sacar(Pagamento pagamento)
        {
            await _ctx.Pagamento.AddAsync(pagamento);
            await _ctx.SaveChangesAsync();
        }
    }
}