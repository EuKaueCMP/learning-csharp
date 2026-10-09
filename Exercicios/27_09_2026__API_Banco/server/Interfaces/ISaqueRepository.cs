using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ISaqueRepository
    {
        public Task<List<saque>> Listar();
        public Task<List<saque>> ListarSaquesPorUsuarioId(int usuarioId);
        public Task<saque> ObterSaquePorId(int saqueId);
        public Task Sacar(saque saque);

    }
}