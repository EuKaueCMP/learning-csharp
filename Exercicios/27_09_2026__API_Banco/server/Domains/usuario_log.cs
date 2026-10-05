using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class usuario_log
{
    public int log_id { get; set; }

    public int usuario_id { get; set; }

    public tipo_alteracao_enum tipo_alteracao { get; set; }

    public string senha_anterior { get; set; } = null!;

    public string nome_anterior { get; set; } = null!;

    public string email_anterior { get; set; } = null!;

    public DateTime data_alteracao { get; set; }

    public virtual usuario usuario { get; set; } = null!;
}
