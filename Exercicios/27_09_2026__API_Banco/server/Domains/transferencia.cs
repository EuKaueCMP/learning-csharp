using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class transferencia
{
    public int transferencia_id { get; set; }

    public int usuario_remetente_id { get; set; }

    public int usuario_destinatario_id { get; set; }

    public decimal valor { get; set; }

    public DateTime? data_criacao { get; set; }

    public DateTime data_transferencia { get; set; }

    public tipo_transferencia_enum tipo_transferencia { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

    public virtual ICollection<log_transferencia> log_transferencia { get; set; } = new List<log_transferencia>();

    public virtual usuario usuario_destinatario { get; set; } = null!;

    public virtual usuario usuario_remetente { get; set; } = null!;
}
