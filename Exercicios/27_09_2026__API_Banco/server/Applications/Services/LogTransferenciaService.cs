using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs;
using BancoAPI.Interfaces;

namespace BancoAPI.Services
{
    public class LogTransferenciaService
    {
        private readonly ILogTransferenciaRepository _repository;
        public LogTransferenciaService(ILogTransferenciaRepository repository) => _repository = repository;

        public async Task<List<ListarLogTransferenciaDTO>> Listar()
        {
            List<log_transferencia> logs = await _repository.Listar();
            if (logs == null)
                throw new DomainException("Nenhum log de transfenrencia encontrado!");

            return logs.Select(nl => ConvertToDto.LogTransferenciaToDto(nl)) .ToList();
        }

        public async Task<List<ListarLogTransferenciaDTO>> ObterPorUsuarioId(int usuarioId)
        {
            List<log_transferencia> logs = await _repository.ObterPorUsuarioId(usuarioId);
            if (logs == null)
                throw new DomainException("Nenhum log de transferencia encontrado!");

            return logs.Select(lw => ConvertToDto.LogTransferenciaToDto(lw)).ToList();
        }

        public async Task<List<ListarLogTransferenciaDTO>> ObterPorStatus(string status)
        {
            List<log_transferencia> logs = await _repository.ObterPorStatus(status) ?? throw new DomainException("Log de transferencia nao localizado!");
            return logs.Select(lw => ConvertToDto.LogTransferenciaToDto(lw)).ToList();
        }

        public async Task<List<ListarLogTransferenciaDTO>> ObterPorUsuarioIdStatus(int usuarioId, string status)
        {
            List<log_transferencia> logs = (await _repository.ObterPorUsuarioIdStatus(usuarioId, status)
                            ?? throw new DomainException("Log de transferencia nao localizado!"));

            return logs.Select(lw => ConvertToDto.LogTransferenciaToDto(lw)).ToList();
        }

        public async Task<ListarLogTransferenciaDTO> ObterPorId(int usuarioid)
        {
            return ConvertToDto.LogTransferenciaToDto(await _repository.ObterPorId(usuarioid)
                            ?? throw new DomainException("Log de transferencia nao localizado!"));
        }
    }
}