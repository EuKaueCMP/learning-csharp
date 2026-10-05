using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class conta_usuario
{
    public long numero_conta { get; set; }

    public int usuario_id { get; set; }

    public string numero_agencia { get; set; } = null!;

    public decimal? saldo { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
