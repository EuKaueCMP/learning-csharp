using BancoAPI.Domains;

namespace BancoAPI.DTOs.PagamentoDTO;

public partial class ListarPagamentoDto
{
    public int PagamentoId { get; set; }

    public int UsuarioId { get; set; }

    public string NomeUsuario { get; set; } = string.Empty!;

    public decimal Valor { get; set; }

    public long NumeroConta { get; set; }

    public string AgenciaConta { get; set; } = null!;

    public tipo_pagamento_enum TipoPagamento { get; set; }

    public status_movimentacao_enum StatusPagamento { get; set; }
}