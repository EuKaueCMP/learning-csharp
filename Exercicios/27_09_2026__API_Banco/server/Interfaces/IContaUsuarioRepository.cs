using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface IContaUsuarioRepository
    {
        public Task<List<ContaUsuario>> Listar();

        public Task<ContaUsuario> ObterContaPorId(int contaId);
        
        public Task<ContaUsuario> ObterContaPorNumero(int numeroConta, int agenciaConta);

        public Task<decimal> ObterSaldo(int numeroConta, int agenciaConta);
    }
}