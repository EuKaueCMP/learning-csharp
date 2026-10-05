namespace BancoAPI.DTO
{
    public partial class ListarMovimentacaoDTO
    {
        public int movimentacao_id { get; set; }

        public int? usuario_id { get; set; }

        public string tipo_movimentacao { get; set; }

        public decimal saldo_anterior { get; set; }

        public decimal saldo_atual { get; set; }

        public DateTime? data_movimentacao { get; set; }

    }
}