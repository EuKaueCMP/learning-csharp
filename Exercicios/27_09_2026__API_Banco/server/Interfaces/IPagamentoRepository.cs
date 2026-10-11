using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IPagamentoRepository
    {
        public Task<List<Pagamento>> Listar();
        public Task<List<Pagamento>> ListarPagamentosPorUsuarioId(int usuarioId);
        public Task<Pagamento> ObterPagamentoPorId(int pagamentoId);
        public Task Sacar(Pagamento pagamento);
    }
}