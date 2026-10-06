using BancoAPI.Domains;
using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.DTOs.LogTransferenciaDTO;
using BancoAPI.DTOs.TransferenciaDTO;
using BancoAPI.DTOs.UsuarioDTO;
using BancoAPI.DTOs.UsuarioLogDTO;

namespace BancoAPI.Applications.Conversions
{
    public static class ConvertToDto
    {
        public static ListarLogTransferenciaDTO LogTransferenciaToDto(log_transferencia logTransf)
        {
            return new ListarLogTransferenciaDTO
            {
                log_id = logTransf.log_id,
                transferencia_id = logTransf.transferencia_id,
                data_alteracao = logTransf.data_alteracao,
                descricao_log = logTransf.descricao_log,
                status_transferencia = logTransf.status_movimentacao_anterior.ToString()
            };
        }

        public static ListarMovimentacaoDTO MovimentacaoToDto(movimentacao movimentacao)
        {
            return new ListarMovimentacaoDTO
            {
                movimentacao_id = movimentacao.movimentacao_id,
                usuario_id = movimentacao.usuario_id,
                tipo_movimentacao = movimentacao.tipo_movimentacao.ToString(),
                saldo_anterior = movimentacao.saldo_anterior,
                saldo_atual = movimentacao.saldo_atual,
                data_movimentacao = movimentacao.data_movimentacao
            };
        }
        
        public static ListarTransferenciaDTO TransferenciaToDto(transferencia transferencia)
        {
            return new ListarTransferenciaDTO
            {
                transferencia_id = transferencia.transferencia_id,
                nome_remetente = transferencia.usuario_remetente.nome,
                usuario_remetente_id = transferencia.usuario_remetente_id,
                nome_destinatario = transferencia.usuario_destinatario.nome,
                usuario_destinatario_id = transferencia.usuario_destinatario_id,
                data_criacao = transferencia.data_criacao,
                data_transferencia = transferencia.data_transferencia,
                tipo_transferencia = transferencia.tipo_transferencia.ToString(),
                status_transferencia = transferencia.status_movimentacao.ToString()
            };
        }
        
        public static ListarUsuarioDTO UsuarioToDto(usuario usuario)
        {
            return new ListarUsuarioDTO
            {
                usuario_id = usuario.usuario_id,
                nome = usuario.nome,
                email = usuario.email
            };
        }

        public static ListarUsuarioLogDTO UsuarioLogToDto(usuario_log usuarioLog)
        {
            return new ListarUsuarioLogDTO
            {
                log_id = usuarioLog.log_id,
                usuario_id = usuarioLog.usuario_id,
                tipo_alteracao = usuarioLog.tipo_alteracao.ToString(),
                nome_anterior = usuarioLog.nome_anterior,
                nome_alteracao = usuarioLog.usuario.nome,
                email_anterior = usuarioLog.email_anterior,
                email_alteracao = usuarioLog.usuario.email,
                valor = (Decimal)usuarioLog.usuario.conta_usuario.saldo,
                data_alteracao = usuarioLog.data_alteracao,
            };
        }
    }
}