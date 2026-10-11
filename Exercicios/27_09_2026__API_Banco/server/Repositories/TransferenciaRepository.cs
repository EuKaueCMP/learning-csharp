using BancoAPI.Applications.Conversions;
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

        public Task<List<Transferencia>> Listar()
                             => _ctx.Transferencia
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioId(int usuarioId)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.UsuarioRemetente)
                                .Include(t => t.UsuarioDestinatario)
                                .Where(t => t.UsuarioRemetenteId == usuarioId || t.UsuarioDestinatarioId == usuarioId)
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransferenciaPorUsurioIdData(int usuarioId, DateOnly data)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.UsuarioRemetente)
                                .Include(t => t.UsuarioDestinatario)
                                .Where(t => DateTimeToOnly.ToOnly(t.DataTransferencia) == data)
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioIdTipo(int usuarioId, string tipo)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.UsuarioDestinatario)
                                .Where(t => t.UsuarioRemetenteId == usuarioId && t.tipo_transferencia.ToString() == tipo.ToUpper())
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioIdStatus(int usuarioId, string status)
                            => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.UsuarioRemetente)
                                .Include(t => t.UsuarioDestinatario)
                                .Where(t => t.UsuarioRemetenteId == usuarioId && t.status_movimentacao.ToString() == status.ToUpper())
                                .ToListAsync();
        public async Task<Transferencia> ObterTransferenciaPorId(int id)
                            => await _ctx.Transferencia
                                .Include(t => t.UsuarioRemetente)
                                .Include(t => t.UsuarioDestinatario)
                                .Where(t => t.TransferenciaId == id).FirstAsync();

        public async Task Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor, DateOnly dataTransferencia, string status)
        {
            Transferencia transf = new Transferencia
            {
                DataTransferencia = Convert.ToDateTime(dataTransferencia),
                UsuarioRemetenteId = usuarioRemetenteId,
                UsuarioDestinatarioId = usuarioDestinatarioId,
                Valor = valor,
                tipo_transferencia = Enum.Parse<tipo_transferencia_enum>(tipoTransferencia),
                status_movimentacao = Enum.Parse<status_movimentacao_enum>(status)
            };

            await _ctx.Transferencia.AddAsync(transf);
            await  _ctx.SaveChangesAsync();   
        }
    }
}