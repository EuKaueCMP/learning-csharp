using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public TipoUsuarioRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<List<tipo_usuario>> Listar() 
                            => _ctx.tipo_usuario
                                .ToListAsync();

        public async Task<tipo_usuario> ObterPorId(int id)
                             => await _ctx.tipo_usuario
                                .FindAsync(id);

        public async Task<bool> ObterPorNome(string nome)
                             =>  await _ctx.tipo_alteracao
                                .AnyAsync(ta => ta.nome_alteracao == nome);

        public async void Adicionar(tipo_usuario tipoUsu)
        {
            await _ctx.tipo_usuario.AddAsync(tipoUsu);
            await _ctx.SaveChangesAsync();
        }

        public async void Atualizar(tipo_usuario tipoUsu)
        {
            _ctx.tipo_usuario.Update(tipoUsu);
            await _ctx.SaveChangesAsync();
        }
    }
}