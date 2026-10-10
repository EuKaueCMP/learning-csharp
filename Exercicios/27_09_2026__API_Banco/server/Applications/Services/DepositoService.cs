namespace BancoAPI.Services
{
    public class DepositoService 
    {
         private readonly IDepositoRepository _repository;

         public DepositoService(IDepositoRepository repository);
         
         public async Task<List<deposito>> Listar() 
         {
              List<deposito> depositos = await _repository.Listar() ?? throw new DomainException("Erro!, nenhum depósito localizado!);
              
              return ConvertToDto.DepositoToDto(deposito);
         }
    }
}