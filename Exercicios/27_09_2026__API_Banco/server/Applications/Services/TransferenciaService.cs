using System.Security.Claims;
using BancoAPI;
using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.Interfaces;

namespace BancoAPi
{
    public class TransferenciaService
    {
        private readonly ITransferenciaRepository _repository;
        private readonly IStatusTransferenciaRepository _statusRepository;
        public TransferenciaService(ITransferenciaRepository repository, IStatusTransferenciaRepository statusRepository)
        {
            _repository = repository;
            _statusRepository = statusRepository;
        }

        public async Task<List<ListarTransferenciaDTO>> ObterPorUsuarioRemetenteId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new DomainException("Nenhuma transferencia localizada");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioId(usuarioId);

            return transferencias.Select(t => ConvertToDto.TransferenciaToDto(t)).ToList();
        }

        public async Task<List<ListarTransferenciaDTO>> ObterPorUsuarioDestinatarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioId(usuarioId);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<List<ListarTransferenciaDTO>> ObterTransfrerenciaPorUsurioIdData(int usuarioId, DateOnly dataTransferencia)
        {
            if (usuarioId <= 0)
            throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsurioIdData(usuarioId, dataTransferencia);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<List<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioIdTipoId(int usuarioId, int tipoId)
        {
            if (usuarioId <= 0 || tipoId <= 0)
                throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioIdTipoId(usuarioId, tipoId);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<List<ListarTransferenciaDTO>> ObteTransferenciaPorUsuarioId(int usuarioId, int statusId)
        {
            if (usuarioId <= 0 || statusId <= 0)
                throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioIdTipoId(usuarioId, statusId);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<ListarTransferenciaDTO> ObterPorTransferenciaId(int transferenciaId)
        {
            return ConvertToDto.TransferenciaToDto(await _repository.ObterTransferenciaPorId(transferenciaId)
                                 ?? throw new DomainException("Nenhuma transferencia encontrada!"));
        }

        public async void Transferir(int tipoTransferenciaId, int usuarioRemetenteId, int usuarioDestinatarioId, double saldo)
        {
            if (usuarioRemetenteId <= 0)
                throw new DomainException("Erro, usuario remetente nao localizado!");

            DateOnly dataTransferencia = DateOnly.FromDateTime(DateTime.Now);
            string statusTranserencia = "EM ANDAMENTO";
            if ( tipoTransferenciaId <= 0 || saldo <= 0)
                throw new DomainException("Preencha todos os valores para seguir com a transferencia!");

            var status = await _statusRepository.ObterPorNome(statusTranserencia);
            _repository.Transferir(tipoTransferenciaId, usuarioRemetenteId, usuarioDestinatarioId, saldo, dataTransferencia, status.status_transferencia_id);
        }
    }
}