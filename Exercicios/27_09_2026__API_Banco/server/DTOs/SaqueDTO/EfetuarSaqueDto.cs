using BancoAPI.Domains;

namespace BancoAPI.DTOs.SaqueDTO;

public partial class EfetuarSaqueDto
{
    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public decimal Valor { get; set; }

    public string? LocalizacaoSaque { get; set; }

    public tipo_saque_enum TipoSaque { get; set; }

    public status_movimentacao_enum StatusMovimentacao { get; set; }

}