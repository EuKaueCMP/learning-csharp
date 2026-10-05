using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IMovimentacaoRepository
    {
        public Task<List<movimentacao>> Listar();
        public Task<List<movimentacao>> ObterPorUsuarioId(int usuarioId);
        public Task<List<movimentacao>> ObterPorData(DateOnly data);
        public Task<List<movimentacao>> ObterTransfrerenciaPorUsurioIdData(int usarioId, DateOnly data);
        public Task<movimentacao> ObterPorId(int id);
    }
}