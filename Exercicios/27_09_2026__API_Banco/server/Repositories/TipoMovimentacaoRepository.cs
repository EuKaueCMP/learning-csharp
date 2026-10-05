using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class TipoMovimentacaoRepository : ITipoMovimentacaoRepository
    {
        private readonly AppDbContext _ctx;
        public TipoMovimentacaoRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<List<tipo_movimentacao>> Listar()
                                 => _ctx.tipo_movimentacao
                                    .ToListAsync();

        public async Task<tipo_movimentacao> ObterPorId(int tipoId)
                                 => await _ctx.tipo_movimentacao
                                    .FindAsync(tipoId);
                                    
        public async Task<bool> ObterPorNome(string nome)
                                 =>  await _ctx.tipo_alteracao
                                    .AnyAsync(ta => ta.nome_alteracao == nome);

        public async void Adicionar(tipo_movimentacao tipoMovimentacao)
        {
            await _ctx.tipo_movimentacao.AddAsync(tipoMovimentacao);
            await _ctx.SaveChangesAsync();
        }

        public async void Atualizar(tipo_movimentacao tipoMovimentacao)
        {
            _ctx.tipo_movimentacao.Update(tipoMovimentacao);
            await _ctx.SaveChangesAsync();
        }
    }
}
