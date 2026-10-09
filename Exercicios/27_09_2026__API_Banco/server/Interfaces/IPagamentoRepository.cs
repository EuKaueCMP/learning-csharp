using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IPagamentoRepository
    {
        public Task<List<pagamento>> Listar();
        public Task<List<pagamento>> ListarPagamentosPorUsuarioId(int usuarioId);
        public Task<pagamento> ObterPagamentoPorId(int pagamentoId);
        public Task Sacar(pagamento pagamento);
    }
}