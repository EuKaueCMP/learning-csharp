using BancoAPI.Applications.Conversions;
using BancoAPI.Domains;
using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.Interfaces;
using BancoAPI.Exceptions;

namespace BancoAPI.Applications.Services
{
    public class MovimentacaoService
    {
        private readonly IMovimentacaoRepository _repository;
        public MovimentacaoService(IMovimentacaoRepository repository) => _repository = repository;

        public async Task<List<ListarMovimentacaoDTO>> Listar()
        {
            List<movimentacao> movimentacoes = await _repository.Listar() ?? throw new DomainException("Nenhuma movimentação encontrada!");
            return movimentacoes.Select(mv => ConvertToDto.MovimentacaoToDto(mv)).ToList();
        }

        public async Task<List<ListarMovimentacaoDTO>> ObterPorUsuarioId(int usuarioId)
        {
            List<movimentacao> movimentacoes = await _repository.ObterPorUsuarioId(usuarioId) ?? throw new DomainException("Nenhuma movimentação encontrada!");
            return movimentacoes.Select(mv => ConvertToDto.MovimentacaoToDto(mv)).ToList();
        }

        public async Task<List<ListarMovimentacaoDTO>> obterPorData(DateOnly data)
        {
            List<movimentacao> movimentacoes = await _repository.ObterPorData(data) ?? throw new DomainException("Nenhuma movimentação encontrada!");
            return movimentacoes.Select(mv => ConvertToDto.MovimentacaoToDto(mv)).ToList();
        }

        public async Task<List<ListarMovimentacaoDTO>> ObterTransfrerenciaPorUsurioIdData(int usuarioid, DateOnly data)
        {
            List<movimentacao> movimentacoes = await _repository.ObterTransfrerenciaPorUsurioIdData(usuarioid, data) ?? throw new DomainException("Nenhuma movimentação encontrada!");
            return movimentacoes.Select(mv => ConvertToDto.MovimentacaoToDto(mv)).ToList();
        }

        public async Task<ListarMovimentacaoDTO> ObterPorId(int movimentacaoId)
        {
            return ConvertToDto.MovimentacaoToDto(await _repository.ObterPorId(movimentacaoId) ?? throw new DomainException("Nenhuma movimentação localizada!"));
        }
    }
}