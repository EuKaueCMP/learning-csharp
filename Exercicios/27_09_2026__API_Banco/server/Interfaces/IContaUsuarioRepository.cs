using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IContaUsuarioRepository
    {
        public Task<List<ContaUsuario>> Listar();

        public Task<ContaUsuario> ObterContaPorNumeroContaAgencia(int numeroConta, string agenciaConta);

        public Task<decimal> ObterSaldo(int numeroConta, string agenciaConta);
    }
}