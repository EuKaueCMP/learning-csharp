using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Movimentacao
{
    public int MovimentacaoId { get; set; }

    public int UsuarioId { get; set; }

    public string? Informacoes { get; set; }

    public tipo_movimentacao_enum tipo_movimentacao { get; set; }

    public decimal SaldoMovimentado { get; set; }

    public decimal SaldoAnterior { get; set; }

    public decimal SaldoAtual { get; set; }

    public DateTime? DataMovimentacao { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
