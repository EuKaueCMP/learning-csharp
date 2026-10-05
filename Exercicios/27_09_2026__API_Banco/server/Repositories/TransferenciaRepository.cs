using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class TransferenciaRepository : ITransferenciaRepository
    {
        private readonly AppDbContext _ctx;
        public TransferenciaRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<List<transferencia>> Listar()
                             => _ctx.transferencia
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsuarioId(int usuarioId)
                             => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo)
                                .Include(t => t.status)
                                .Where(t => t.usuario_remetente_id == usuarioId || t.usuario_destinatario_id == usuarioId)
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsurioIdData(int usuarioId, DateOnly data)
                             => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo)
                                .Include(t => t.status)
                                .Where(t => DateTimeToOnly.ToOnly(t.data_transferencia ?? DateTime.Now) == data)
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdTipoId(int usuarioId, int tipoId)
                             => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo)
                                .Include(t => t.status)
                                .Where(t => t.usuario_remetente_id == usuarioId && t.tipo_id == tipoId)
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdStatusId(int usuarioId, int statusId) 
                            => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo)
                                .Include(t => t.status)
                                .Where(t => t.usuario_remetente_id == usuarioId && t.status_id == statusId)
                                .ToListAsync();
        public async Task<transferencia> ObterTransferenciaPorId(int id) 
                            => await _ctx.transferencia                                                            .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo)
                                .Include(t => t.status)
                                .FirstOrDefaultAsync(t => t.transferencia_id == id);

        public void Transferir(int tipoTransferenciaId, int usuarioRemetenteId, int usuarioDestinatarioId, double saldo, DateOnly dataTransferencia, int statusId)
        {
            transferencia transf = new transferencia
            {
                data_transferencia = Convert.ToDateTime(dataTransferencia),
                usuario_remetente_id = usuarioRemetenteId,
                usuario_destinatario_id = usuarioDestinatarioId,
                tipo_id = tipoTransferenciaId,
                status_id = statusId
            };
        }
    }
}