namespace BancoAPI.DTOs.TransferenciaDTO
{
    public partial class ListarTransferenciaDTO
    {
        public int transferencia_id { get; set; }

        public string nome_remetente { get; set; }
        public long contaRemetente { get; set; }

        public string nome_destinatario { get; set; }
        public long contaDestinatario { get; set; }
        public DateTime? data_criacao { get; set; }
        public DateTime? data_transferencia { get; set; }

        public string tipo_transferencia { get; set; }

        public string status_transferencia { get; set; }
    }
}