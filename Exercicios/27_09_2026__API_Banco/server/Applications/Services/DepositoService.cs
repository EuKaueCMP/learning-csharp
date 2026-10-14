using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.DepositoDTO;
using BancoAPI.Exceptions;
using BancoAPI.Interfaces;

namespace BancoAPI.Services
{
    public class DepositoService
    {
        private readonly IDepositoRepository _repository;

        public DepositoService(IDepositoRepository repository) => _repository = repository;

        public async Task<List<ListarDepositoDTO>> Listar()
        {
            List<Deposito> depositos = await _repository.Listar()
                                                ?? throw new DomainException("Erro!, nenhum depósito localizado!");


            return depositos.Select(d => ConvertToDto.DepositoToDto(d)).ToList();
        }

        public async Task<List<ListarDepositoDTO>> ObterDepositoPorUsuarioId(int usuarioId)
        {
            List<Deposito> depositos = await _repository.ListarDepositoPorUsuarioId(usuarioId)
                                                ?? throw new DomainException("Erro, nenhum deposito localizado!");

            return depositos.Select(d => ConvertToDto.DepositoToDto(d)).ToList();
        }

        public async Task<ListarDepositoDTO> ObterDepositoPorId(int depositoId)
                => ConvertToDto.DepositoToDto(await _repository.ObterDepositoPorId(depositoId) ?? throw new DomainException("Erro, nenhum deposito localizado!"));

        public async Task Depositar(DepositarDto deposito)
        {
            if (deposito.NumeroConta <= 0 || string.IsNullOrWhiteSpace(deposito.NumeroAgencia) || deposito.Valor <= 0 ||
                 string.IsNullOrWhiteSpace(deposito.status_movimentacao.ToString()) ||
                 string.IsNullOrWhiteSpace(deposito.tipo_deposito.ToString()))
                    throw new DomainException("Erro, Preencha todos os campos sao obrigatórios!");

            await _repository.Depositar(deposito.NumeroConta, deposito.NumeroAgencia, deposito.Valor, deposito.tipo_deposito, deposito.status_movimentacao);
            
        }
    }
}