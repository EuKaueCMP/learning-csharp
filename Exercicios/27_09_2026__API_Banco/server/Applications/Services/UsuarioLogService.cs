using BancoAPI;
using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.UsuarioLogDTO;
using BancoAPI.Interfaces;
using BancoAPI.Exceptions;

namespace BancoAPI.Applications.Services
{
    public class UsuarioLogService
    {
        private readonly IUsuarioLogRepository _repository;
        public UsuarioLogService(IUsuarioLogRepository repository) => _repository = repository;

        public async Task<List<ListarUsuarioLogDTO>> Listar()
        {
            List<usuario_log> logs = await _repository.Listar()
                ?? throw new DomainException("Nenhum logs de usuario encontrado!");

            return logs.Select(ul => ConvertToDto.UsuarioLogToDto(ul)).ToList();
        }
        public async Task<ListarUsuarioLogDTO> ObterPorId(int logUsuId) => ConvertToDto.UsuarioLogToDto(await _repository.ObterLogPorId(logUsuId) ?? throw new DomainException("Nenhum log do usuario encontrado!"));

        public async Task<List<ListarUsuarioLogDTO>> ObterLogPorUsuarioId(int usuarioId)
        {
            List<usuario_log> logs = await _repository.ObterLogPorUsuarioId(usuarioId) 
                    ?? throw new DomainException("Nenhum log do usuario encontrado!");
            return logs.Select(ul => ConvertToDto.UsuarioLogToDto(ul)).ToList();
        }
    }
}