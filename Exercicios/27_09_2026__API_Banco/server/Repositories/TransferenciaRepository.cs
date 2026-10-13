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

        public Task<List<Transferencia>> ObterTransfPorNumConta(int numeroConta)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.ContaRemetente)
                                .Include(t => t.ContaDestinatario)
                                .Where(t => t.ContaRemetenteId == numeroConta || t.ContaDestinatarioId == numeroConta)
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransfPorNumContaData(int numeroConta, DateOnly data)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.ContaRemetente)
                                .Include(t => t.ContaDestinatario)
                                .Where(t => DateTimeToOnly.ToOnly(t.DataTransferencia) == data && t.ContaRemetenteId == numeroConta || t.ContaDestinatarioId == numeroConta)
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransfPorNumContaTipo(int numeroConta, string tipo)
                             => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.ContaDestinatario)
                                .Where(t => t.ContaRemetenteId == numeroConta  || t.ContaDestinatarioId == numeroConta && t.tipo_transferencia.ToString() == tipo.ToUpper())
                                .ToListAsync();

        public Task<List<Transferencia>> ObterTransfPorNumContaStatus(int numeroConta, string status)
                            => _ctx.Transferencia
                                .OrderByDescending(t => t.DataTransferencia)
                                .Include(t => t.ContaRemetente)
                                .Include(t => t.ContaDestinatario)
                                .Where(t => t.ContaRemetenteId == numeroConta || t.ContaDestinatarioId == numeroConta && t.status_movimentacao.ToString() == status.ToUpper())
                                .ToListAsync();
        public async Task<Transferencia> ObterTransferenciaPorId(int id)
                            => await _ctx.Transferencia
                                .Include(t => t.ContaRemetente)
                                .Include(t => t.ContaDestinatario)
                                .Where(t => t.TransferenciaId == id).FirstAsync();

        public async Task Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor, DateOnly dataTransferencia, string status)
        {
            Transferencia transf = new Transferencia
            {
                DataTransferencia = Convert.ToDateTime(dataTransferencia),
                ContaRemetenteId = usuarioRemetenteId,
                ContaDestinatarioId = usuarioDestinatarioId,
                Valor = valor,
                tipo_transferencia = Enum.Parse<tipo_transferencia_enum>(tipoTransferencia),
                status_movimentacao = Enum.Parse<status_movimentacao_enum>(status)
            };

            await _ctx.Transferencia.AddAsync(transf);
            await  _ctx.SaveChangesAsync();   
        }
    }
}