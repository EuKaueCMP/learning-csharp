namespace BancoAPI.DTOs
{
    public partial class ListarUsuarioLogDTO
    {
        public int log_id { get; set; }

        public int usuario_id { get; set; }

        public string nome_alteracao { get; set; }
        public int tipo_alteracao_id { get; set; }

        public string nome { get; set; } = null!;

        public string email { get; set; } = null!;

        public decimal saldo { get; set; }

        public DateTime? data_alteracao { get; set; }
    }
}