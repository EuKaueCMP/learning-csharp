using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class LogTransferencia
{
    public int LogId { get; set; }

    public int? TransferenciaId { get; set; }

    public string DescricaoLog { get; set; } = null!;

    public status_movimentacao_enum status_movimentacao_anterior { get; set; }


    public DateTime? DataAlteracao { get; set; }

    public virtual Transferencia? Transferencia { get; set; }
}
