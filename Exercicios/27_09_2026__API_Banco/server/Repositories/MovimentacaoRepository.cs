using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly AppDbContext _ctx;
        public MovimentacaoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<movimentacao>> Listar()
                             => await _ctx.movimentacao
                                .OrderByDescending(m => m.data_movimentacao)
                                .Include(m => m.usuario)
                                .Include(m => m.tipo_movimentacao)
                                .ToListAsync();

        public async Task<List<movimentacao>> ObterPorUsuarioId(int usuarioId)
                            => await _ctx.movimentacao
                                .OrderByDescending(m => m.data_movimentacao)
                                .Include(m => m.usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(m => m.usuario_id == usuarioId)
                                .ToListAsync();

        public async Task<List<movimentacao>> ObterPorData(DateOnly data)
                             => await _ctx.movimentacao
                                .OrderByDescending(m => m.data_movimentacao)
                                .Include(m => m.usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(l => DateTimeToOnly.ToOnly(l.data_movimentacao
                                    ?? DateTime.Now) == data)
                                .ToListAsync();

        public async Task<List<movimentacao>> ObterTransfrerenciaPorUsurioIdData(int usuarioId, DateOnly data)
                             => await _ctx.movimentacao
                                .OrderByDescending(m => m.data_movimentacao)
                                .Include(m => m.usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(m => m.usuario_id == usuarioId && DateTimeToOnly.ToOnly(m.data_movimentacao
                                    ?? DateTime.Now) == data)
                                .ToListAsync();

        public async Task<movimentacao> ObterPorId(int id)
                             => await _ctx.movimentacao
                                .Include(m => m.usuario)
                                .Include(m => m.tipo_movimentacao)
                                .FirstOrDefaultAsync(m => m.movimentacao_id == id);
    }
}