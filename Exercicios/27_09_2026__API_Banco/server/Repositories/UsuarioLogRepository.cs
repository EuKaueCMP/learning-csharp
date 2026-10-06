using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class UsuarioLogRepository : IUsuarioLogRepository
    {
        private readonly AppDbContext _ctx;
        public UsuarioLogRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<List<usuario_log>> Listar()
                            => _ctx.usuario_log
                                .OrderByDescending(ul => ul.data_alteracao)
                                .ToListAsync();

        public Task<List<usuario_log>> ObterLogPorUsuarioId(int usuarioId)
                             => _ctx.usuario_log
                                .Include(l => l.usuario)
                                .Where(l => l.usuario_id == usuarioId)
                                .OrderByDescending(l => l.data_alteracao)
                                .ToListAsync();
                                
        public async Task<usuario_log> ObterLogPorId(int id)
                            => await _ctx.usuario_log
                                .Include(l => l.usuario)
                                .Where(l => l.log_id == id).FirstAsync();
    }
}