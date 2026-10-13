using System;
using System.Collections.Generic;

namespace BancoAPI.Domains;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public bool Status { get; set; }

    public tipo_usuario_enum TipoUsuario { get; set; }

    public virtual ContaUsuario? ContaUsuario { get; set; }

    public virtual ICollection<Movimentacao> Movimentacao { get; set; } = new List<Movimentacao>();

    public virtual ICollection<UsuarioLog> UsuarioLog { get; set; } = new List<UsuarioLog>();
}
