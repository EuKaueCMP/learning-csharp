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

        public async Task<List<LogTransferencia>> Listar() 
                            => await _ctx.LogTransferencia
                                .OrderByDescending(l => l.DataAlteracao)
                                .Include(u => u)
                                .ToListAsync();

        public async Task<List<LogTransferencia>> ObterPorUsuarioId(int numeroConta)
                            => await _ctx.LogTransferencia
                                .OrderByDescending(l => l.DataAlteracao)
                                .Include(u => u.Transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.Transferencia.ContaRemetenteId == numeroConta || l.Transferencia.ContaDestinatarioId == numeroConta)
                                .ToListAsync();

        public async Task<List<LogTransferencia>> ObterPorStatus(string nomeStatus)
                            => await _ctx.LogTransferencia
                                .OrderByDescending(l => l.DataAlteracao)
                                .Include(u => u.Transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.status_movimentacao_anterior.ToString() == nomeStatus.ToUpper())
                                .ToListAsync();

        public async Task<List<LogTransferencia>> ObterPorUsuarioIdStatus(int numeroConta, string status)
                             => await _ctx.LogTransferencia
                                .OrderByDescending(l => l.DataAlteracao)
                                .Include(u => u.Transferencia)
                                .Include(u => u.status_movimentacao_anterior)
                                .Where(l => l.Transferencia.ContaRemetenteId == numeroConta && l.status_movimentacao_anterior.ToString() == status.ToUpper())
                                .ToListAsync();

        public async Task<LogTransferencia> ObterPorId(int logId)
                             => await _ctx.LogTransferencia
                                .FirstAsync(l => l.LogId == logId);
    }
}