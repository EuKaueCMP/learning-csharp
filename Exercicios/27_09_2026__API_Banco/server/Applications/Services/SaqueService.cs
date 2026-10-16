using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.SaqueDTO;
using BancoAPI.Exceptions;
using BancoAPI.Interfaces;

namespace BancoApi.Services
{
    public class SaqueService
    {
        private readonly ISaqueRepository _repository;

        public SaqueService(ISaqueRepository repository) => _repository = repository;

        public async Task<List<ListarSaqueDto>> Listar()
        {
            List<Saque> saques = await _repository.Listar() ?? throw new DomainException("Erro, Nenhum saque encontrado!");

            return saques.Select(p => ConvertToDto.SaqueToDto(p)).ToList();
        }

        public async Task<List<ListarSaqueDto>> ListarSaquePorAgenciaConta(long numeroConta, string numeroAgencia)
        {
            List<Saque> saques = await _repository.ListarSaquePorContaAgencia(numeroConta, numeroAgencia) ?? throw new DomainException("Erro, nenhum saque encontrado.");

            return saques.Select(p => ConvertToDto.SaqueToDto(p)).ToList();
        }

        public async Task<ListarSaqueDto> ObterSaquePorId(int saqueId) => ConvertToDto.SaqueToDto(await _repository.ObterSaquePorId(saqueId) ?? throw new DomainException("Erro, nenhum saque encontrado."));

        public async Task Sacar(EfetuarSaqueDto novoSaquue)
        {
            if (novoSaquue.NumeroConta <= 0 || string.IsNullOrWhiteSpace(novoSaquue.AgenciaConta) || novoSaquue.Valor <= 0)
                throw new DomainException("Erro, Preencha todos os campos!");

            Saque saque = new Saque
            {
                AgenciaConta = novoSaquue.AgenciaConta,
                NumeroConta = novoSaquue.NumeroConta,
                Valor = novoSaquue.Valor,
                LocalizacaoSaque = novoSaquue.LocalizacaoSaque,
            };

            await _repository.Sacar(saque);
        }
    }
}