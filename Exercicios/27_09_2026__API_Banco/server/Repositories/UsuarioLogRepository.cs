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

        public Task<List<UsuarioLog>> Listar()
                            => _ctx.UsuarioLog
                                .OrderByDescending(ul => ul.DataAlteracao)
                                .ToListAsync();

        public Task<List<UsuarioLog>> ObterLogPorUsuarioId(int usuarioId)
                             => _ctx.UsuarioLog
                                .Include(l => l.Usuario)
                                .Where(l => l.UsuarioId == usuarioId)
                                .OrderByDescending(l => l.DataAlteracao)
                                .ToListAsync();
                                
        public async Task<UsuarioLog> ObterLogPorId(int id)
                            => await _ctx.UsuarioLog
                                .Include(l => l.Usuario)
                                .Where(l => l.LogId == id).FirstAsync();
    }
}