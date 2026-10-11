using BancoAPI.Domains;
using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.DTOs.LogTransferenciaDTO;
using BancoAPI.DTOs.TransferenciaDTO;
using BancoAPI.DTOs.UsuarioDTO;
using BancoAPI.DTOs.UsuarioLogDTO;
using BancoAPI.DTOs.DepositoDTO;

namespace BancoAPI.Applications.Conversions
{
    public static class ConvertToDto
    {
        // public static ListarDepositoDTO DepositoToDto(deposito deposito)
        // {
        //     return new ListarDepositoDTO
        //     {
                
        //     };
        // }

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
                nome_remetente = transferencia.UsuarioRemetente.Nome,
                usuario_remetente_id = transferencia.UsuarioRemetenteId,
                nome_destinatario = transferencia.UsuarioDestinatario.Nome,
                usuario_destinatario_id = transferencia.UsuarioDestinatarioId,
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