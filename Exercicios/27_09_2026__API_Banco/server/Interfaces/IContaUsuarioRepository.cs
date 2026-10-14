using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IContaUsuarioRepository
    {
        public Task<List<ContaUsuario>> Listar();

        public Task<ContaUsuario> ObterContaPorNumeroContaAgencia(long numeroConta, string agenciaConta);

        public Task<decimal> ObterSaldo(long numeroConta, string agenciaConta);
    }
}