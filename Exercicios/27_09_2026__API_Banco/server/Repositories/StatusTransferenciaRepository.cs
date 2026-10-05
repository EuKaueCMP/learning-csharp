using System.Security.Cryptography.X509Certificates;
using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BancoAPI.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly AppDbContext _ctx;
        public StatusTransferenciaRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<status_transferencia>> Listar()
                             => await _ctx.status_transferencia
                                .ToListAsync();

        public async Task<status_transferencia> ObterPorId(int id)
                             => await _ctx.status_transferencia
                                .FindAsync(id);

        public async Task<status_transferencia> ObterPorNome(string nomeStatus)
                             => await _ctx.status_transferencia
                                .FirstOrDefaultAsync(st => st.nome_status == nomeStatus);

        public async void Adicionar(status_transferencia statusTransf)
        {
            await _ctx.status_transferencia.AddAsync(statusTransf);
            await _ctx.SaveChangesAsync();
        }

        public async void Atualizar(status_transferencia statusTransf)
        {
            _ctx.status_transferencia.Update(statusTransf);
            await _ctx.SaveChangesAsync();
        }
    }
}