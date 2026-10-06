namespace BancoAPI.DTOs.TransferenciaDTO
{
    public partial class CriarTransferenciaUsuarioDTO
    {
        public int? usuario_remetente_id { get; set; }

        public int? usuario_destinatario_id { get; set; }

        public int? tipo_id { get; set; }

        public int? status_id { get; set; }
    }
}