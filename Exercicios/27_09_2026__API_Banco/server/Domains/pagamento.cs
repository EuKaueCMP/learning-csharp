using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class pagamento
{
    public int pagamento_id { get; set; }

    public int usuario_id { get; set; }

    public tipo_pagamento_enum tipo_pagamento { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

    public decimal valor { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
