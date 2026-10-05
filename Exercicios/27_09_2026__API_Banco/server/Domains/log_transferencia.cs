using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class log_transferencia
{
    public int log_id { get; set; }

    public int? transferencia_id { get; set; }

    public string descricao_log { get; set; } = null!;

    public status_movimentacao_enum status_movimentacao_anterior { get; set; } 

    public DateTime? data_alteracao { get; set; }

    public virtual transferencia? transferencia { get; set; }
}
