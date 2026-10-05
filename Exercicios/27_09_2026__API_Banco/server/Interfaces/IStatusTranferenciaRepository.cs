using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IStatusTransferenciaRepository
    {
        public Task<List<status_transferencia>> Listar();
        public Task<status_transferencia> ObterPorId(int id);
        public Task<status_transferencia> ObterPorNome(string nomeStatus);
        public void Adicionar(status_transferencia statusTransferencia);
        public void Atualizar(status_transferencia statusTransferencia);
    }
}