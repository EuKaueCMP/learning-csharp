using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.ContaUsuarioDTO;
using BancoAPI.Exceptions;
using BancoAPI.Interfaces;
using BancoAPI.Repositories;

namespace BancoAPI.Services
{
    public class ContaUsuarioService
    {
        private readonly IContaUsuarioRepository _repository;
        public ContaUsuarioService(ContaUsuarioRepository repository) => _repository = repository;

        public async Task<List<ListarContaUsuarioDto>> Listar()
        {
            List<ContaUsuario> contas = await _repository.Listar()
                                            ?? throw new DomainException("Erro, nenhuma conta localizada!");
            return contas.Select(c => ConvertToDto.ContaUsuarioToDto(c)).ToList();
        }

        public async Task<ListarContaUsuarioDto> ObterContaPorNumeroContaAgencia(long numeroConta, string numeroAgencia) => ConvertToDto.ContaUsuarioToDto(await _repository.ObterContaPorNumeroContaAgencia(numeroConta, numeroAgencia)) 
                                            ?? throw new DomainException("Erro, nenhuma conta localizada!");

        public async Task<decimal> ObterSaldo(long numeroConta, string agenciaConta)
        {
            if(numeroConta <= 0 || string.IsNullOrWhiteSpace(agenciaConta))
                throw new DomainException("Erro, nenhuma conta localizada!");

            decimal saldo = await _repository.ObterSaldo(numeroConta, agenciaConta);
            return saldo;
        }
        
    }
}