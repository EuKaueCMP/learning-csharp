using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class LogTransferenciaRepository : ILogTransferenciaRepository
    {
        private readonly AppDbContext _ctx;
        public LogTransferenciaRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<log_transferencia>> Listar() 
                            => await _ctx.log_transferencia
                                .OrderByDescending(l => l.data_alteracao)
                                .Include(u => u)
                                .ToListAsync();

        public async Task<List<log_transferencia>> ObterPorUsuarioId(int usuarioId)
                            => await _ctx.log_transferencia
                                .OrderByDescending(l => l.data_alteracao)
                                .Include(u => u.transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.transferencia.usuario_remetente_id == usuarioId || l.transferencia.usuario_destinatario_id == usuarioId)
                                .ToListAsync();

        public async Task<List<log_transferencia>> ObterPorStatus(string nomeStatus)
                            => await _ctx.log_transferencia
                                .OrderByDescending(l => l.data_alteracao)
                                .Include(u => u.transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.status_movimentacao_anterior.ToString() == nomeStatus.ToUpper())
                                .ToListAsync();

        public async Task<List<log_transferencia>> ObterPorUsuarioIdStatus(int usuarioId, string status)
                             => await _ctx.log_transferencia
                                .OrderByDescending(l => l.data_alteracao)
                                .Include(u => u.transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.transferencia.usuario_remetente_id == usuarioId && l.status_movimentacao_anterior.ToString() == status.ToUpper())
                                .ToListAsync();

        public async Task<log_transferencia> ObterPorId(int logId)
                             => await _ctx.log_transferencia
                                .FirstAsync(l => l.log_id == logId);
    }
}