using BancoAPI.Domains;

namespace BancoAPI.DTOs.PagamentoDTO;

public partial class EfetuarPagamentoDto
{
    
    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public DateTime? DataPagamento { get; set; }

    public decimal Valor { get; set; }

    public tipo_pagamento_enum tipo_pagamento { get; set; }

    public status_movimentacao_enum status_movimentacao { get; set; }

}