using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class saque
{
    public int saque_id { get; set; }

    public int usuario_id { get; set; }

    public decimal valor { get; set; }

    public tipo_saque_enum tipo_saque { get; set; } 
    
    public status_movimentacao_enum status_movimentacao { get; set; }

    public DateTime data_saque { get; set; }

    public string? localizacao_saque { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
