using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class movimentacao
{
    public int movimentacao_id { get; set; }

    public int usuario_id { get; set; }

    public string? informacoes { get; set; }

    public tipo_movimentacao_enum tipo_movimentacao { get; set; }

    public decimal saldo_movimentado { get; set; }

    public decimal saldo_anterior { get; set; }

    public decimal saldo_atual { get; set; }

    public DateTime? data_movimentacao { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
