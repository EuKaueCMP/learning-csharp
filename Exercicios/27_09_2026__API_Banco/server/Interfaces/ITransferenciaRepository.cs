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
        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioId(int usuarioId);
        public Task<List<Transferencia>> ObterTransferenciaPorUsurioIdData(int usuarioid, DateOnly data);
        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioIdTipo(int usuarioId, string tipo);
        public Task<List<Transferencia>> ObterTransferenciaPorUsuarioIdStatus(int usuarioId, string status);
        public Task<Transferencia> ObterTransferenciaPorId(int id);
        public Task Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor, DateOnly dataTransferencia, string status);
    }
}