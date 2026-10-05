using BancoAPI.Domains;

namespace BancoAPI.Interfaces
{
    public interface ITransferenciaRepository
    {
        public Task<List<transferencia>> Listar();
        //? Busca toda e qualquer transferencia
        //? que menciona o usuarioId estar
        //? Exemplo:
        //? Listagem de 
        public Task<List<transferencia>> ObterTransferenciaPorUsuarioId(int usuarioId);
        public Task<List<transferencia>> ObterTransferenciaPorUsurioIdData(int usuarioid, DateOnly data);
        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdTipoId(int usuarioId, int tipoId);
        public Task<List<transferencia>> ObterTransferenciaPorUsuarioIdStatusId(int usuarioId, int statusId);
        public Task<transferencia> ObterTransferenciaPorId(int id);
        public void Transferir(int tipoTransferenciaId, int usuarioRemetenteId, int usuarioDestinatarioId, double saldo, DateOnly dataTransferencia, int statusId);
    }
}