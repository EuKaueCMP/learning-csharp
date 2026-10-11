using BancoAPI.Domains;

namespace BancoAPI.DTOs.DepositoDTO;

public partial class ListarDepositoDTO
{
    public int deposito_id { get; set; }

    public int usuario_id { get; set; }

    public decimal valor { get; set; }

    public tipo_deposito_enum tipo_deposito { get; set; } 

    public status_movimentacao_enum status_movimentacao { get; set; }

    public DateTime data_deposito { get; set; }

    public virtual Usuario usuario { get; set; } = null!;
}