using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Transferencia
{
    public int TransferenciaId { get; set; }

    public int UsuarioRemetenteId { get; set; }

    public int UsuarioDestinatarioId { get; set; }

    public decimal Valor { get; set; }

    public DateTime? DataCriacao { get; set; }

    public DateTime DataTransferencia { get; set; }

    public tipo_transferencia_enum tipo_transferencia { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

    public virtual ICollection<LogTransferencia> LogTransferencia { get; set; } = new List<LogTransferencia>();

    public virtual Usuario UsuarioDestinatario { get; set; } = null!;

    public virtual Usuario UsuarioRemetente { get; set; } = null!;
}
