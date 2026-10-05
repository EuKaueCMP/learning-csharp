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
                status_id = logTransf.status_id
            };
        }

        public static ListarMovimentacaoDTO MovimentacaoToDto(movimentacao movimentacao)
        {
            return new ListarMovimentacaoDTO
            {
                movimentacao_id = movimentacao.movimentacao_id,
                usuario_id = movimentacao.usuario_id,
                tipo_movimentacao_id = movimentacao.tipo_movimentacao_id,
                saldo_anterior = movimentacao.saldo_anterior,
                saldo_atual = movimentacao.saldo_atual,
                data_movimentacao = movimentacao.data_movimentacao
            };
        }

        public static ListarStatusTransferenciaDTO StatusTransferenciaToDto(status_transferencia statusTransferencia)
        {
            return new ListarStatusTransferenciaDTO
            {
                status_transferencia_id = statusTransferencia.status_transferencia_id,
                nome_status = statusTransferencia.nome_status
            };
        }

        public static ListarTipoAlteracaoDTO TipoAltacaoToDto(tipo_alteracao tipoAlteracao)
        {
            return new ListarTipoAlteracaoDTO
            {
                tipo_alteracao_id = tipoAlteracao.tipo_alteracao_id,
                nome_alteracao = tipoAlteracao.nome_alteracao
            };
        }

        public static ListarTipoMovimentacaoDTO TipoMovimentacaoToDto(tipo_movimentacao tipoMovimentacao)
        {
            return new ListarTipoMovimentacaoDTO
            {
                tipo_movimentacao_id = tipoMovimentacao.tipo_movimentacao_id,
                tipo = tipoMovimentacao.tipo
            };
        }

        public static ListarTipoTransferenciaDTO TipoTransferenciaToDto(tipo_transferencia tipoTransferencia)
        {
            return new ListarTipoTransferenciaDTO
            {
                tipo_transferencia_id = tipoTransferencia.tipo_transferencia_id,
                nome_tipo = tipoTransferencia.nome_tipo
            };
        }

        public static ListarTipoUsuarioDTO TipoUsuarioToDto(tipo_usuario tipoUsuario)
        {
            return new ListarTipoUsuarioDTO
            {
                tipo_usuario_id = tipoUsuario.tipo_usuario_id,
                nome_tipo = tipoUsuario.tipo
            };
        }

        public static ListarTransferenciaDTO TransferenciaToDto(transferencia transferencia)
        {
            return new ListarTransferenciaDTO
            {
                transferencia_id = transferencia.transferencia_id,
                status_id = transferencia.status_id,
                tipo_id = transferencia.tipo_id,
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
                nome = logUsu.nome,
                email = logUsu.email,
                saldo = logUsu.saldo,
                nome_alteracao = logUsu.tipo_alteracao.nome_alteracao,
                tipo_alteracao_id = logUsu.tipo_alteracao_id,
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