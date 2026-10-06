namespace BancoAPI.DTOs.UsuarioDTO
{
    public partial class CriarUsuarioDTO
    {
        public string nome { get; set; } = null!;

        public string email { get; set; } = null!;

        public string senha { get; set; } = null!;

        public int? tipo_usuario_id { get; set; }
    }
}