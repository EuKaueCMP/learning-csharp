namespace BancoAPI.DTOs.UsuarioDTO
{
    public partial class ListarUsuarioDTO
    {
        public int usuario_id { get; set; }

        public string nome { get; set; } = null!;

        public string email { get; set; } = null!;
    }
}