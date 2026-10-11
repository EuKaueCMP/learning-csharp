using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Pagamento
{
    public int PagamentoId { get; set; }

    public int UsuarioId { get; set; }

    public tipo_pagamento_enum tipo_pagamento { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }


    public decimal Valor { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
