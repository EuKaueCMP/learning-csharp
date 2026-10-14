using BancoAPI.Domains;
using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.DTOs.LogTransferenciaDTO;
using BancoAPI.DTOs.TransferenciaDTO;
using BancoAPI.DTOs.UsuarioDTO;
using BancoAPI.DTOs.UsuarioLogDTO;
using BancoAPI.DTOs.DepositoDTO;
using BancoAPI.DTOs.ContaUsuarioDTO;
using BancoAPI.DTOs.SaqueDTO;
using BancoAPI.DTOs.PagamentoDTO;

namespace BancoAPI.Applications.Conversions
{
    public static class ConvertToDto
    {
        public static ListarContaUsuarioDto ContaUsuarioToDto(ContaUsuario contaUsuario)
        {
            return new ListarContaUsuarioDto
            {
                NumeroAgencia = contaUsuario.NumeroAgencia,
                NumeroConta = contaUsuario.NumeroConta,
                UsuarioId = contaUsuario.UsuarioId,
                nomeUsuario = contaUsuario.Usuario.Nome
            };
        }

        public static ListarDepositoDTO DepositoToDto(Deposito deposito)
        {
            return new ListarDepositoDTO
            {
                DepositoId = deposito.DepositoId,
                UsuarioId = deposito.NumeroContaNavigation.UsuarioId,
                nomeUsuario = deposito.NumeroContaNavigation.Usuario.Nome,
                NumeroAgencia = deposito.NumeroAgencia,
                NumeroConta = deposito.NumeroConta,
                TipoDeposito = deposito.tipo_deposito,
                StatusDeposito = deposito.status_movimentacao,
                DataDeposito = deposito.DataDeposito
            };
        }

        public static ListarSaqueDto SaqueToDto(Saque saque)
        {
            return new ListarSaqueDto
            {
                SaqueId = saque.SaqueId,
                UsuarioId = saque.NumeroContaNavigation.UsuarioId,
                nomeUsuario = saque.NumeroContaNavigation.Usuario.Nome,
                AgenciaConta = saque.AgenciaConta,
                NumeroConta = saque.NumeroConta,
                TipoSaque = saque.TipoSaque,
                StatusSaque = saque.StatusMovimentacao,
                DataSaque = saque.DataSaque
            };
        }

        public static ListarPagamentoDto PagamentoToDto(Pagamento pagamento)
        {
            return new ListarPagamentoDto
            {
                PagamentoId = pagamento.PagamentoId,
                UsuarioId = pagamento.NumeroContaNavigation.UsuarioId,
                NomeUsuario = pagamento.NumeroContaNavigation.Usuario.Nome,
                Valor = pagamento.Valor,
                NumeroConta = pagamento.NumeroConta,
                AgenciaConta = pagamento.AgenciaConta,
                TipoPagamento = pagamento.tipo_pagamento,
                StatusPagamento = pagamento.status_movimentacao,
                DataPagamento = pagamento.DataPagamento
            };
        }

        public static ListarLogTransferenciaDTO LogTransferenciaToDto(LogTransferencia logTransf)
        {
            return new ListarLogTransferenciaDTO
            {
                log_id = logTransf.LogId,
                transferencia_id = logTransf.TransferenciaId,
                data_alteracao = logTransf.DataAlteracao,
                descricao_log = logTransf.DescricaoLog,
                status_transferencia = logTransf.status_movimentacao_anterior.ToString()
            };
        }

        public static ListarMovimentacaoDTO MovimentacaoToDto(Movimentacao movimentacao)
        {
            return new ListarMovimentacaoDTO
            {
                movimentacao_id = movimentacao.MovimentacaoId,
                usuario_id = movimentacao.UsuarioId,
                tipo_movimentacao = movimentacao.tipo_movimentacao.ToString(),
                saldo_anterior = movimentacao.SaldoAnterior,
                saldo_atual = movimentacao.SaldoAtual,
                data_movimentacao = movimentacao.DataMovimentacao
            };
        }

        public static ListarTransferenciaDTO TransferenciaToDto(Transferencia transferencia)
        {
            return new ListarTransferenciaDTO
            {
                transferencia_id = transferencia.TransferenciaId,
                nome_remetente = transferencia.ContaRemetente.Usuario.Nome,
                contaRemetente = transferencia.ContaRemetente.UsuarioId,
                nome_destinatario = transferencia.ContaRemetente.Usuario.Nome,
                contaDestinatario = transferencia.ContaDestinatarioId,
                data_criacao = transferencia.DataCriacao,
                data_transferencia = transferencia.DataTransferencia,
                tipo_transferencia = transferencia.tipo_transferencia.ToString(),
                status_transferencia = transferencia.status_movimentacao.ToString()
            };
        }

        public static ListarUsuarioDTO UsuarioToDto(Usuario usuario)
        {
            return new ListarUsuarioDTO
            {
                usuario_id = usuario.UsuarioId,
                nome = usuario.Nome,
                email = usuario.Email
            };
        }

        public static ListarUsuarioLogDTO UsuarioLogToDto(UsuarioLog usuarioLog)
        {
            return new ListarUsuarioLogDTO
            {
                log_id = usuarioLog.LogId,
                usuario_id = usuarioLog.UsuarioId,
                tipo_alteracao = usuarioLog.TipoAlteracao.ToString(),
                nome_anterior = usuarioLog.NomeAnterior,
                nome_alteracao = usuarioLog.Usuario.Nome,
                email_anterior = usuarioLog.EmailAnterior,
                email_alteracao = usuarioLog.Usuario.Email,
                valor = (Decimal)usuarioLog.Usuario.ContaUsuario.Saldo,
                data_alteracao = usuarioLog.DataAlteracao,
            };
        }
    }
}