using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.PagamentoDTO;
using BancoAPI.Exceptions;
using BancoAPI.Interfaces;

namespace BancoAPI.Services
{
    public class PagamentoService
    {
        private readonly IPagamentoRepository _repository;

        public PagamentoService(IPagamentoRepository repository) => _repository = repository;

        public async Task<List<ListarPagamentoDto>> Listar()
        {
            List<Pagamento> pagamentos = await _repository.Listar() ?? throw new DomainException("Erro, Nenhum pagamento encontrado!");

            return pagamentos.Select(p => ConvertToDto.PagamentoToDto(p)).ToList();
        }

        public async Task<List<ListarPagamentoDto>> ListarPagamentoPorAgenciaConta(int numeroConta, string numeroAgencia)
        {
            List<Pagamento> pagamentos = await _repository.ListarPagamentosPorAgenciaConta(numeroConta, numeroAgencia)
                                            ?? throw new DomainException("Erro, nenhum pagamento encontrado.");

            return pagamentos.Select(p => ConvertToDto.PagamentoToDto(p)).ToList();
        }

        public async Task<ListarPagamentoDto> ObterPagamentoPorId(int pagamentoId)
                                     => ConvertToDto.PagamentoToDto(await _repository.ObterPagamentoPorId(pagamentoId) 
                                            ?? throw new DomainException("Erro, nenhum pagamento encontrado."));

        public async Task Sacar(long numeroConta, string agenciaConta, decimal valorSaque)
        {
            if(numeroConta <= 0 || string.IsNullOrWhiteSpace(agenciaConta) || valorSaque <= 0)
                throw new DomainException("Erro, Preencha todos os campos!");

            Pagamento pagamento = new Pagamento
            {
                AgenciaConta = agenciaConta,
                NumeroConta = numeroConta,
                Valor = valorSaque
            };

            await _repository.Sacar(pagamento);
        }
    }
}