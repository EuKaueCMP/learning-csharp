using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class deposito
{
    public int deposito_id { get; set; }

    public int usuario_id { get; set; }

    public decimal valor { get; set; }

    public tipo_deposito_enum tipo_deposito { get; set; } 

    public status_movimentacao_enum status_movimentacao { get; set; }

    public DateTime data_deposito { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
