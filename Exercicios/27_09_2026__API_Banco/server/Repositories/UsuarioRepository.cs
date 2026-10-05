using BancoAPI.Contexts;
using BancoAPI.Domains;
using BancoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public UsuarioRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<List<usuario>> Listar()
                            => await _ctx.usuario
                                .Include(u => u.tipo_usuario)
                                .ToListAsync();
        public async Task<List<usuario>> ObterUsuarioPorTipoId(int tipoId)
                         => await _ctx.usuario
                            .Where(u => u.tipo_usuario_id == tipoId)
                            .Include(u => u.tipo_usuario)
                            .ToListAsync();

        public async Task<usuario> ObterUsuarioPorId(int usuarioId)
                         => await _ctx.usuario
                            .Include(u => u.tipo_usuario)
                            .FirstOrDefaultAsync(u => u.tipo_usuario_id == usuarioId);
        public async void Adicionar(usuario usuario)
        {
            await _ctx.usuario.AddAsync(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async void Atualizar(usuario usuario)
        {
            _ctx.usuario.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async void AtualizarSenha(int usuarioId, string senha)
        {
            usuario usuario = await _ctx.usuario.FindAsync(usuarioId);
            usuario.senha = senha;

            _ctx.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async void Remover(usuario usuario)
                            => _ctx.usuario
                                .Remove(usuario);
    }
}