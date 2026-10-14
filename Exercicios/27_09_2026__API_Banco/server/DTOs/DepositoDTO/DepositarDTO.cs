using BancoAPI.Domains;

namespace BancoAPI.DTOs.DepositoDTO;

public partial class DepositarDto
{
    public int DepositoId { get; set; }

    public long NumeroConta { get; set; }

    public string? NumeroAgencia { get; set; }

    public decimal Valor { get; set; }

    public tipo_deposito_enum tipo_deposito { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

}