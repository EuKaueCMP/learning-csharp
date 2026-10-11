using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Deposito
{
    public int DepositoId { get; set; }

    public long NumeroConta { get; set; }

    public string? NumeroAgencia { get; set; }

    public decimal Valor { get; set; }

    public tipo_deposito_enum tipo_deposito { get; set; } 

    public status_movimentacao_enum status_movimentacao { get; set; }

    public DateTime DataDeposito { get; set; }

    public virtual ContaUsuario NumeroContaNavigation { get; set; } = null!;
}
