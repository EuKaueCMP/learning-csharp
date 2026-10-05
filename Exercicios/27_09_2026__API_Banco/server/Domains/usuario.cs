using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class usuario
{
    public int usuario_id { get; set; }

    public string nome { get; set; } = null!;

    public string email { get; set; } = null!;

    public string senha { get; set; } = null!;

    public bool status { get; set; }

    public tipo_usuario_enum tipo_usuario { get; set; }

    public virtual conta_usuario? conta_usuario { get; set; }

    public virtual ICollection<deposito> deposito { get; set; } = new List<deposito>();

    public virtual ICollection<movimentacao> movimentacao { get; set; } = new List<movimentacao>();

    public virtual ICollection<pagamento> pagamento { get; set; } = new List<pagamento>();

    public virtual ICollection<saque> saque { get; set; } = new List<saque>();

    public virtual ICollection<transferencia> transferenciausuario_destinatario { get; set; } = new List<transferencia>();

    public virtual ICollection<transferencia> transferenciausuario_remetente { get; set; } = new List<transferencia>();

    public virtual ICollection<usuario_log> usuario_log { get; set; } = new List<usuario_log>();
}
