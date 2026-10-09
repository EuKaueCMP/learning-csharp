using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IDepositoRepository
    {
        public Task<List<deposito>> Listar();
        public Task<List<deposito>> ListarDepositoPorUsuarioId(int usuarioId);
        public Task<deposito> ObterDepositoPorId(int depositoId);
        public Task Depositar(deposito deposito);
    }
}