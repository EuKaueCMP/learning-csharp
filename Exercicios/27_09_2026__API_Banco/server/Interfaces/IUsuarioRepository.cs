using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<List<usuario>> Listar();
        public Task<List<usuario>> ObterUsuarioPorTipoId(string tipo);
        public Task<usuario> ObterUsuarioPorId(int id);
        public Task<usuario> ObterUsuarioPorEmail(string email);
        public Task Adicionar(usuario usuario);
        public Task Atualizar(usuario usuario);
        public Task AtualizarSenha(int id, string senha);
        public Task Remover(usuario usuario);
    }
}