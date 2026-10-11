using BancoAPI.Applications.Conversions;
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

        public async Task<List<Movimentacao>> Listar()
                             => await _ctx.Movimentacao
                                .OrderByDescending(m => m.DataMovimentacao)
                                .Include(m => m.Usuario)
                                .Include(m => m.tipo_movimentacao)
                                .ToListAsync();

        public async Task<List<Movimentacao>> ObterPorUsuarioId(int usuarioId)
                            => await _ctx.Movimentacao
                                .OrderByDescending(m => m.DataMovimentacao)
                                .Include(m => m.Usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(m => m.UsuarioId == usuarioId)
                                .ToListAsync();

        public async Task<List<Movimentacao>> ObterPorData(DateOnly data)
                             => await _ctx.Movimentacao
                                .OrderByDescending(m => m.DataMovimentacao)
                                .Include(m => m.Usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(l => DateTimeToOnly.ToOnly(l.DataMovimentacao
                                    ?? DateTime.Now) == data)
                                .ToListAsync();

        public async Task<List<Movimentacao>> ObterTransfrerenciaPorUsurioIdData(int usuarioId, DateOnly data)
                             => await _ctx.Movimentacao
                                .OrderByDescending(m => m.DataMovimentacao)
                                .Include(m => m.Usuario)
                                .Include(m => m.tipo_movimentacao)
                                .Where(m => m.UsuarioId == usuarioId && DateTimeToOnly.ToOnly(m.DataMovimentacao
                                    ?? DateTime.Now) == data)
                                .ToListAsync();

        public async Task<Movimentacao> ObterPorId(int id)
                             => await _ctx.Movimentacao
                                .Include(m => m.Usuario)
                                .Include(m => m.tipo_movimentacao)
                                .FirstAsync(m => m.MovimentacaoId == id);
    }
}