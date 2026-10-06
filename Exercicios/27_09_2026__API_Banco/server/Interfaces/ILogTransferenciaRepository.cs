using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ILogTransferenciaRepository
    {
        public Task<List<log_transferencia>> Listar();
        public Task<log_transferencia> ObterPorId(int id);
        public Task<List<log_transferencia>> ObterPorUsuarioId(int usuarioId);
        public Task<List<log_transferencia>> ObterPorStatus(string status);
        public Task<List<log_transferencia>> ObterPorUsuarioIdStatus(int usuarioId, string status);
    }
}