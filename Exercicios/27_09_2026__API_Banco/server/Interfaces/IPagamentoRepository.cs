using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IPagamentoRepository
    {
        public Task<List<Pagamento>> Listar();
        public Task<List<Pagamento>> ListarPagamentosPorAgenciaConta(long numeroConta, string numeroAgencia);
        public Task<Pagamento> ObterPagamentoPorId(int pagamentoId);
        public Task EfetuarPagamento(Pagamento pagamento);
    }
}