using BancoAPI.Domains;

namespace BancoAPI.DTOs.SaqueDTO;

public partial class ListarSaqueDto
{
    public int SaqueId { get; set; }

    public int UsuarioId {get; set;}
    public string nomeUsuario {get; set;} = string.Empty!;
    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public decimal Valor { get; set; }
    
    public tipo_saque_enum TipoSaque { get; set; }

    public status_movimentacao_enum StatusSaque { get; set; }

    public DateTime DataSaque { get; set; }

    public string? LocalizacaoSaque { get; set; }
}