using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ISaqueRepository
    {
        public Task<List<Saque>> Listar();
        public Task<List<Saque>> ListarSaquesPorUsuarioId(int usuarioId);
        public Task<Saque> ObterSaquePorId(int saqueId);
        public Task Sacar(Saque saque);

    }
}