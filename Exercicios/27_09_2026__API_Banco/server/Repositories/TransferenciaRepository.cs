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
                                .Include(t => t.tipo_transferencia)
                                .Include(t => t.status_movimentacao)
                                .Where(t => t.usuario_remetente_id == usuarioId || t.usuario_destinatario_id == usuarioId)
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsurioIdData(int usuarioId, DateOnly data)
                             => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo_transferencia)
                                .Include(t => t.status_movimentacao)
                                .Where(t => DateTimeToOnly.ToOnly(t.data_transferencia) == data)
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdTipoId(int usuarioId, string tipo)
                             => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia)
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo_transferencia)
                                .Include(t => t.status_movimentacao)
                                .Where(t => t.usuario_remetente_id == usuarioId && t.tipo_transferencia.ToString() == tipo.ToUpper())
                                .ToListAsync();

        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdStatusId(int usuarioId, string status)
                            => _ctx.transferencia
                                .OrderByDescending(t => t.data_transferencia).Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo_transferencia)
                                .Include(t => t.status_movimentacao)
                                .Where(t => t.usuario_remetente_id == usuarioId && t.status_movimentacao.ToString() == status.ToUpper())
                                .ToListAsync();
        public async Task<transferencia> ObterTransferenciaPorId(int id)
                            => await _ctx.transferencia
                                .Include(t => t.usuario_remetente)
                                .Include(t => t.usuario_destinatario)
                                .Include(t => t.tipo_transferencia)
                                .Include(t => t.status_movimentacao)
                                .FirstOrDefaultAsync(t => t.transferencia_id == id);

        public void Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, double saldo, DateOnly dataTransferencia, string status)
        {
            transferencia transf = new transferencia
            {
                data_transferencia = Convert.ToDateTime(dataTransferencia),
                usuario_remetente_id = usuarioRemetenteId,
                usuario_destinatario_id = usuarioDestinatarioId,
                tipo_transferencia = Enum.Parse<tipo_transferencia_enum>(tipoTransferencia),
                status_movimentacao = Enum.Parse<status_movimentacao_enum>(status)
            };
        }
    }
}