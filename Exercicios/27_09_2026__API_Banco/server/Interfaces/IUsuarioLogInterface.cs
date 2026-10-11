using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IUsuarioLogRepository
    {
        public Task<List<UsuarioLog>> Listar();
        public Task<UsuarioLog> ObterLogPorId(int id);
        public Task<List<UsuarioLog>> ObterLogPorUsuarioId(int usuarioId);
    }
}