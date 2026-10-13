using BancoAPI.Domains;
using BancoAPI.DTOs.MovimentacaoDTO;
using BancoAPI.DTOs.LogTransferenciaDTO;
using BancoAPI.DTOs.TransferenciaDTO;
using BancoAPI.DTOs.UsuarioDTO;
using BancoAPI.DTOs.UsuarioLogDTO;
using BancoAPI.DTOs.DepositoDTO;
using BancoAPI.DTOs.ContaUsuarioDTO;

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