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
        public async Task<List<usuario>> ObterUsuarioPorTipoId(string tipo)
                         => await _ctx.usuario
                            .Where(u => u.tipo_usuario.ToString() == tipo.ToUpper())
                            .Include(u => u.tipo_usuario)
                            .ToListAsync();

        public async Task<usuario> ObterUsuarioPorId(int usuarioId)
                         => await _ctx.usuario
                            .Include(u => u.tipo_usuario)
                            .FirstAsync(u => u.usuario_id == usuarioId);

        public async Task<usuario> ObterUsuarioPorEmail(string email)
                        => await _ctx.usuario
                            .FirstAsync(u => u.email == email);

        public async Task Adicionar(usuario usuario)
        {
            await _ctx.usuario.AddAsync(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task Atualizar(usuario usuario)
        {
            _ctx.usuario.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task AtualizarSenha(int usuarioId, string senha)
        {
            usuario usuario = await _ctx.usuario.FirstAsync(u => u.usuario_id == usuarioId);
            usuario.senha = senha;

            _ctx.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task Remover(usuario usuario)
        {
            _ctx.usuario.Remove(usuario);
            await _ctx.SaveChangesAsync();
        }

    }
}