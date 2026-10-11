using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class UsuarioLog
{
    public int LogId { get; set; }

    public int UsuarioId { get; set; }

    public tipo_alteracao_enum tipo_alteracao { get; set; }

    public string SenhaAnterior { get; set; } = null!;

    public string NomeAnterior { get; set; } = null!;

    public string EmailAnterior { get; set; } = null!;

    public DateTime DataAlteracao { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
