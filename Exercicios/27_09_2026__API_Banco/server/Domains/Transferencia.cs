using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Transferencia
{
    public int TransferenciaId { get; set; }

    public long ContaRemetenteId { get; set; }

    public long ContaDestinatarioId { get; set; }

    public decimal Valor { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime DataTransferencia { get; set; }

    public tipo_transferencia_enum tipo_transferencia { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

    public virtual ContaUsuario ContaDestinatario { get; set; } = null!;

    public virtual ContaUsuario ContaRemetente { get; set; } = null!;

    public virtual ICollection<LogTransferencia> LogTransferencia { get; set; } = new List<LogTransferencia>();
}
