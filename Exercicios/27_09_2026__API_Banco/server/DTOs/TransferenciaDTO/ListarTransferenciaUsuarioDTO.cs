namespace BancoAPI
{
    public partial class ListarTransferenciaDTO
    {
        public int transferencia_id { get; set; }

        public string nome_remetente { get; set; }
        public int? usuario_remetente_id { get; set; }

        public string nome_destinatario { get; set; }
        public int? usuario_destinatario_id { get; set; }

        public DateTime? data_transferencia { get; set; }

        public int? tipo_id { get; set; }

        public int? status_id { get; set; }
    }
}