using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<List<usuario>> Listar();
        public Task<List<usuario>> ObterUsuarioPorTipoId(int tipoId);
        public Task<usuario> ObterUsuarioPorId(int id);
        public void Adicionar(usuario usuario);
        public void Atualizar(usuario usuario);
        public void AtualizarSenha(int id, string senha);
        public void Remover(usuario usuario);
    }
}