using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class SaqueRepository : ISaqueRepository
    {
        private readonly AppDbContext _ctx;
        public SaqueRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<Saque>> Listar() => await _ctx.Saque.ToListAsync();

        public async Task<List<Saque>> ListarSaquePorContaAgencia(int numeroConta, string agencia) => await _ctx.Saque.OrderBy(s => s.DataSaque).Where(s => s.NumeroConta == numeroConta && s.AgenciaConta == agencia).ToListAsync();

        public async Task<Saque> ObterSaquePorId(int saqueId) => await _ctx.Saque.FirstAsync(s => s.SaqueId == saqueId);

        public async Task Sacar(Saque saque)
        {
            await _ctx.Saque.AddAsync(saque);
            await _ctx.SaveChangesAsync();
        }
    }
}