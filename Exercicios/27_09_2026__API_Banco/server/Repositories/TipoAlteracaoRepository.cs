using System.Reflection.Metadata.Ecma335;
using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly AppDbContext _ctx;
        public TipoAlteracaoRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<tipo_alteracao>> Listar()
                            => await _ctx.tipo_alteracao
                                .ToListAsync();

        public async Task<tipo_alteracao> ObterPorId(int id)
                            => await _ctx.tipo_alteracao
                                .FindAsync(id);

        public async Task<bool> ObterPorNome(string nome)
                             => await _ctx.tipo_alteracao
                                .AnyAsync(ta => ta.nome_alteracao == nome);

        public async void Adicionar(tipo_alteracao tipoAlteracao)
        {
            await _ctx.tipo_alteracao.AddAsync(tipoAlteracao);
            await _ctx.SaveChangesAsync();
        }

        public async void Atualizar(tipo_alteracao tipoAlteracao)
        {
            _ctx.tipo_alteracao.Update(tipoAlteracao);
            await _ctx.SaveChangesAsync();
        }
    }
}