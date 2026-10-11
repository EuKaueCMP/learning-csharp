using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Saque
{
    public int SaqueId { get; set; }

    public decimal Valor { get; set; }

    public tipo_saque_enum tipo_saque { get; set; } 
    
    public status_movimentacao_enum status_movimentacao { get; set; }

    public DateTime DataSaque { get; set; }

    public string? LocalizacaoSaque { get; set; }

    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public virtual ContaUsuario NumeroContaNavigation { get; set; } = null!;
}
