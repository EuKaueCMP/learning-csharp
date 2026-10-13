using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ITransferenciaRepository
    {
        public Task<List<Transferencia>> Listar();
        //? Busca toda e qualquer transferencia
        //? que menciona o usuarioId estar
        //? Exemplo:
        //? Listagem de 
        public Task<List<Transferencia>> ObterTransfPorNumConta(int numeroConta);
        public Task<List<Transferencia>> ObterTransfPorNumContaData(int numeroConta, DateOnly data);
        public Task<List<Transferencia>> ObterTransfPorNumContaTipo(int numeroConta, string tipo);
        public Task<List<Transferencia>> ObterTransfPorNumContaStatus(int numeroConta, string status);
        public Task<Transferencia> ObterTransferenciaPorId(int id);
        public Task Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor, DateOnly dataTransferencia, string status);
    }
}