namespace BancoAPI.DTOs
{
    public partial class ListarUsuarioLogDTO
    {
        public int log_id { get; set; }

        public int usuario_id { get; set; }

        public string nome_alteracao { get; set; } = null!;
        public string tipo_alteracao { get; set; } = null!;

        public string nome_anterior { get; set; } = null!;

        public string email_anterior { get; set; } = null!;

        public decimal saldo { get; set; }

        public DateTime? data_alteracao { get; set; }
    }
}