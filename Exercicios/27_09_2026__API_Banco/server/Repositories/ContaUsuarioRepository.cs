using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class ContaUsuarioRepository : IContaUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public ContaUsuarioRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<ContaUsuario>> Listar() => await _ctx.ContaUsuario.ToListAsync();

        public async Task<ContaUsuario> ObterContaPorNumeroContaAgencia(long numeroConta, string numeroAgencia) => await _ctx.ContaUsuario.FirstAsync(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);

        public async Task<decimal> ObterSaldo(long numeroConta, string numeroAgencia)
        {
            ContaUsuario contaBuscada = await _ctx.ContaUsuario.FirstAsync(c => c.NumeroConta == numeroConta && c.NumeroAgencia == numeroAgencia);
            return contaBuscada.Saldo ?? 0;
        }
    }
}