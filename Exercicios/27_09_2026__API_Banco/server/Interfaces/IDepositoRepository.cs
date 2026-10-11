using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IDepositoRepository
    {
        public Task<List<Deposito>> Listar();
        public Task<List<Deposito>> ListarDepositoPorUsuarioId(int usuarioId);
        public Task<Deposito> ObterDepositoPorId(int depositoId);
        public Task Depositar(int conta, string agencia, decimal valor, tipo_deposito_enum tipo, status_movimentacao_enum status, DateTime data);
    }
}