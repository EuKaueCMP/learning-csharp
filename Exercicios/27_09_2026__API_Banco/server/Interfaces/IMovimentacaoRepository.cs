using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IMovimentacaoRepository
    {
        public Task<List<Movimentacao>> Listar();
        public Task<List<Movimentacao>> ObterPorUsuarioId(int usuarioId);
        public Task<List<Movimentacao>> ObterPorData(DateOnly data);
        public Task<List<Movimentacao>> ObterTransfrerenciaPorUsurioIdData(int usarioId, DateOnly data);
        public Task<Movimentacao> ObterPorId(int id);
    }
}