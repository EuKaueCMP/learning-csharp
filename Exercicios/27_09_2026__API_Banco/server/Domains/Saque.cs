using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Saque
{
    public int SaqueId { get; set; }

    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public decimal Valor { get; set; }

    public tipo_saque_enum TipoSaque { get; set; }

    public status_movimentacao_enum StatusMovimentacao { get; set; }

    public DateTime DataSaque { get; set; }

    public string? LocalizacaoSaque { get; set; }

    public virtual ContaUsuario NumeroContaNavigation { get; set; } = null!;
}
