using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> Listar();
        public Task<List<Usuario>> ObterUsuarioPorTipoId(string tipo);
        public Task<Usuario> ObterUsuarioPorId(int id);
        public Task<Usuario> ObterUsuarioPorEmail(string email);
        public Task Adicionar(Usuario usuario);
        public Task Atualizar(Usuario usuario);
        public Task AtualizarSenha(int id, string senha);
        public Task Remover(Usuario usuario);
    }
}