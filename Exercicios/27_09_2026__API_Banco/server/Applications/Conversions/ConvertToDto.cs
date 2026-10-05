using BancoAPI.Domains;
using BancoAPI.DTO;
using BancoAPI.DTOs;

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
                status_transferencia = transferencia.status_movimentacao.ToString(),
                tipo_transferencia = transferencia.tipo_transferencia.ToString(),
                usuario_remetente_id = transferencia.usuario_remetente_id,
                nome_remetente = transferencia.usuario_remetente.nome,
                usuario_destinatario_id = transferencia.usuario_destinatario_id,
                nome_destinatario = transferencia.usuario_destinatario.nome,
                data_transferencia = transferencia.data_transferencia
            };
        }

        public static ListarUsuarioLogDTO UsuarioLogToDto(usuario_log logUsu)
        {
            return new ListarUsuarioLogDTO
            {
                log_id = logUsu.log_id,
                usuario_id = logUsu.usuario_id,
                nome_anterior = logUsu.nome_anterior,
                email_anterior = logUsu.email_anterior,
                nome_alteracao = logUsu.tipo_alteracao.ToString(),
                tipo_alteracao = logUsu.tipo_alteracao.ToString(),
                data_alteracao = logUsu.data_alteracao
            };
        }

        public static ListarUsuarioDTO UsuarioToDto(usuario usuario)
        {
            return new ListarUsuarioDTO
            {
                usuario_id = usuario.usuario_id,
                nome = usuario.nome,
                email = usuario.email,
            };
        }
    }
}