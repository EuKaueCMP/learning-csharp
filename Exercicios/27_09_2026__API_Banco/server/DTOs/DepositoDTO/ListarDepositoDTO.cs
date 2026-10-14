using BancoAPI.Domains;

namespace BancoAPI.DTOs.DepositoDTO;

public partial class ListarDepositoDTO
{
    public int DepositoId { get; set; }

    public string nomeUsuario { get; set; } = string.Empty!;

    public int UsuarioId { get; set; }

    public long NumeroConta { get; set; }

    public string? NumeroAgencia { get; set; }

    public decimal Valor { get; set; }

    public tipo_deposito_enum TipoDeposito { get; set; }

    public status_movimentacao_enum StatusDeposito { get; set; }

    public DateTime DataDeposito { get; set; }

}