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
        public TransferenciaService(ITransferenciaRepository repository)
        {
            _repository = repository;
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

        public async Task<List<ListarTransferenciaDTO>> ObterTransferenciaPorUsuarioIdTipo(int usuarioId, string tipo)
        {
            if (usuarioId <= 0 || tipo == null)
                throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioIdTipo(usuarioId, tipo);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<List<ListarTransferenciaDTO>> ObteTransferenciaPorUsuarioId(int usuarioId, string status)
        {
            if (usuarioId <= 0 || status == null)
                throw new DomainException("Nenhuma transferencia encontrada!");

            List<transferencia> transferencias = await _repository.ObterTransferenciaPorUsuarioIdTipo(usuarioId, status);
            return transferencias.Select(tr => ConvertToDto.TransferenciaToDto(tr)).ToList();
        }

        public async Task<ListarTransferenciaDTO> ObterPorTransferenciaId(int transferenciaId)
        {
            return ConvertToDto.TransferenciaToDto(await _repository.ObterTransferenciaPorId(transferenciaId)
                                 ?? throw new DomainException("Nenhuma transferencia encontrada!"));
        }

        public async Task Transferir(string tipoTransferencia, int usuarioRemetenteId, int usuarioDestinatarioId, decimal valor)
        {
            if (usuarioRemetenteId <= 0)
                throw new DomainException("Erro, usuario remetente nao localizado!");

            DateOnly dataTransferencia = DateOnly.FromDateTime(DateTime.Now);
            string statusTranserencia = "EM_ANDAMENTO";
            if (tipoTransferencia == null || valor <= 0)
                throw new DomainException("Preencha todos os valores para seguir com a transferencia!");

            _repository.Transferir(tipoTransferencia, usuarioRemetenteId, usuarioDestinatarioId, valor, dataTransferencia, statusTranserencia);
        }
    }
}