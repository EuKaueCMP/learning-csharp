namespace BancoAPI.DTOs.ContaUsuarioDTO;

public partial class ListarContaUsuarioDto
{
    public string NumeroAgencia { get; set; } = null!;

    public long NumeroConta { get; set; }

    public int UsuarioId { get; set; }

    public string nomeUsuario { get; set; } = string.Empty!;

}