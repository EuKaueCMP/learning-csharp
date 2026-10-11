namespace BancoAPI.Services
{
    public class DepositoService 
    {
         private readonly IDepositoRepository _repository;

         public DepositoService(IDepositoRepository repository);
         
         public async Task<List<deposito>> Listar() 
         {
              List<ListarDepostoDTO> depositos = await _repository.Listar() ?? throw new DomainException("Erro!, nenhum depósito localizado!);
              
              return depositos.Select(d => ConvertToDto.DepositoToDto(d));
         }
        
         public async Task<List<ListarDepositoDTO>> ObterDepositoPorUsuarioId(int usuarioId)
         {
             List<deposito> depostos = _repository.ObterDepositoPorUsuarioId() ?? throw new DomainException("Erro, nenhum deposito localizado!");

             return depositos.Select(d => ConvertToDto.DepositoToDto(d));
         }
    }
}