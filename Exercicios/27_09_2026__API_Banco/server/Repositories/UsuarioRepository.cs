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

        public async Task<List<Usuario>> Listar()
                            => await _ctx.Usuario
                                .Include(u => u.TipoUsuario)
                                .ToListAsync();
        public async Task<List<Usuario>> ObterUsuarioPorTipoId(string tipo)
                         => await _ctx.Usuario
                            .Where(u => u.TipoUsuario.ToString() == tipo.ToUpper())
                            .Include(u => u.TipoUsuario)
                            .ToListAsync();

        public async Task<Usuario> ObterUsuarioPorId(int usuarioId)
                         => await _ctx.Usuario
                            .Include(u => u.TipoUsuario)
                            .FirstAsync(u => u.UsuarioId == usuarioId);

        public async Task<Usuario> ObterUsuarioPorEmail(string email)
                        => await _ctx.Usuario
                            .FirstAsync(u => u.Email == email);

        public async Task Adicionar(Usuario usuario)
        {
            await _ctx.Usuario.AddAsync(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            _ctx.Usuario.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task AtualizarSenha(int usuarioId, string senha)
        {
            Usuario usuario = await _ctx.Usuario.FirstAsync(u => u.UsuarioId == usuarioId);
            usuario.Senha = senha;

            _ctx.Update(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task Remover(Usuario usuario)
        {
            _ctx.Usuario.Remove(usuario);
            await _ctx.SaveChangesAsync();
        }

    }
}