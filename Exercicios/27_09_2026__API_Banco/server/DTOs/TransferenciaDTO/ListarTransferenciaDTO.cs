namespace BancoAPI
{
    public partial class ListarTransferenciaDTO
    {
        public int transferencia_id { get; set; }

        public string nome_remetente { get; set; }
        public int? usuario_remetente_id { get; set; }

        public string nome_destinatario { get; set; }
        public int? usuario_destinatario_id { get; set; }
        public DateTime? data_criacao { get; set; }
        public DateTime? data_transferencia { get; set; }

        public string tipo_transferencia { get; set; }

        public string status_transferencia { get; set; }
    }
}