using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IUsuarioLogRepository
    {
        public Task<List<usuario_log>> Listar();
        public Task<usuario_log> ObterLogPorId(int id);
        public Task<List<usuario_log>> ObterLogPorUsuarioId(int usuarioId);
    }
}