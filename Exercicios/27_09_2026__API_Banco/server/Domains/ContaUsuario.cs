using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class ContaUsuario
{
    public long NumeroConta { get; set; }

    public int UsuarioId { get; set; }

    public string NumeroAgencia { get; set; } = null!;

    public decimal? Saldo { get; set; }

    public virtual ICollection<Deposito> Deposito { get; set; } = new List<Deposito>();

    public virtual ICollection<Saque> Saque { get; set; } = new List<Saque>();

    public virtual Usuario Usuario { get; set; } = null!;
}
