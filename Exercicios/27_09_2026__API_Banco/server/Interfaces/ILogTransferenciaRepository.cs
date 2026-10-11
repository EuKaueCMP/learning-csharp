using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ILogTransferenciaRepository
    {
        public Task<List<LogTransferencia>> Listar();
        public Task<LogTransferencia> ObterPorId(int id);
        public Task<List<LogTransferencia>> ObterPorUsuarioId(int usuarioId);
        public Task<List<LogTransferencia>> ObterPorStatus(string status);
        public Task<List<LogTransferencia>> ObterPorUsuarioIdStatus(int usuarioId, string status);
    }
}